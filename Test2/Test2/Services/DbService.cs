using Microsoft.EntityFrameworkCore;
using Test2.Data;
using Test2.Dtos;
using Test2.Models;

namespace Test2.Services;

public class DbService(DatabaseContext context) : IDbService
{
    public async Task<bool> DoesCharacterExist(int id)
    {
        return await context.Characters.AnyAsync(e => e.Id == id);
    }

    public async Task<List<Character>> GetCharacters(int? id)
    {
        return await context.Characters
            .Where(e => id == null || e.Id == id)
            .ToListAsync();
    }

    public List<Backpack> AddItemsToCharacter(int id, List<CharacterItemsPostDto> postDtos)
    {
        var character = context.Characters.Include(character => character.Backpacks).First(e => e.Id == id);
        
        postDtos.ForEach(e => character.Backpacks.Add(new Backpack
        {
            Amount = e.Amount,
            ItemId = e.ItemId,
            CharacterId = id
        }));

        context.SaveChangesAsync();

        return character.Backpacks
            .Where(e => postDtos.Any(e2 => e.ItemId == e2.ItemId))
            .ToList();
    }

    public bool DoAllItemsExist(List<CharacterItemsPostDto> postDtos)
    {
        return postDtos.All(e => context.Items.Any(e2 => e2.Id == e.ItemId));
    }

    public bool CharacterEnoughCapcity(int id, List<CharacterItemsPostDto> postDtos)
    {
        int sum = 0;
        foreach (var e in postDtos) sum += e.Amount;

        return context.Characters.First(e => e.Id == id).CurrentWeight > sum;
    }
}