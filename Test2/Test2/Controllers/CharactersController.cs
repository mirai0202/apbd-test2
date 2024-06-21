using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Test2.Dtos;
using Test2.Services;

namespace Test2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CharactersController(IDbService dbService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCharacters(int? id = null)
    {
        if (id != null &&
            !await dbService.DoesCharacterExist(id.Value))
        {
            return NotFound("No such character");
        }
        
        var characters = await dbService.GetCharacters(id);
        
        return Ok(characters.Select(p => new GetCharactersDto
        {
            FirstName = p.FirstName,
            LastName = p.LastName,
            CurrentWeight = p.CurrentWeight,
            MaxWeight = p.MaxWeight,
            BackpackItemsDtos = p.Backpacks.Select(p => new BackpackItemsDto
            {
               Name = p.Item.Name,
               Weight = p.Item.Weight,
               Amount = p.Amount
            }).ToList(),
            CharacterTitles = p.CharacterTitles.Select(p => new TitlesDto
            {
                Title = p.Title.Name,
                AcquiredAt = p.AcquiredAt
            }).ToList()
        }));
    }

    [HttpPost]
    [Route("{id}/backpacks")]
    public async Task<IActionResult> AddItemsToCharacterBackpack(int id, List<CharacterItemsPostDto> postDtos)
    {
        if (!await dbService.DoesCharacterExist(id))
        {
            return NotFound("No such character");
        }
        
        if (!dbService.CharacterEnoughCapcity(id, postDtos))
        {
            return NotFound("No enough capacity");
        }
        
        if (!dbService.DoAllItemsExist(postDtos))
        {
            return NotFound("Item do not exist");
        }

        dbService.AddItemsToCharacter(id, postDtos);

        var result = postDtos.Select(postDto => new ReturnPostDto
            {
                Amount = postDto.Amount, ItemId = postDto.ItemId, CharacterId = id
            }).ToList();

        return Ok(result);
    }
}