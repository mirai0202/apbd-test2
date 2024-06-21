using Test2.Dtos;
using Test2.Models;

namespace Test2.Services;

public interface IDbService
{
    Task<bool> DoesCharacterExist(int id);
    Task<List<Character>> GetCharacters(int? id);

    List<Backpack> AddItemsToCharacter(int id, List<CharacterItemsPostDto> postDtos);

    bool DoAllItemsExist(List<CharacterItemsPostDto> postDtos);

    bool CharacterEnoughCapcity(int id, List<CharacterItemsPostDto> postDtos);
}