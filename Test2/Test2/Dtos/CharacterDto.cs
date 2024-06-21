namespace Test2.Dtos;

public class GetCharactersDto
{
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;

    public int CurrentWeight { get; set; }

    public int MaxWeight { get; set; }

    public ICollection<BackpackItemsDto> BackpackItemsDtos { get; set; } = null!;

    public ICollection<TitlesDto> CharacterTitles { get; set; } = null!;
}

public class BackpackItemsDto
{
    public string Name { get; set; } = string.Empty;
    public int Weight { get; set; }
    public int Amount { get; set; }
}

public class TitlesDto
{
    public string Title { get; set; } = string.Empty;
    public DateTime AcquiredAt { get; set; }
}

public class CharacterItemsPostDto
{
    public int ItemId { get; set; }
    public int Amount { get; set; }
}

public class ReturnPostDto
{
    public int CharacterId { get; set; }
    public int ItemId { get; set; }
    public int Amount { get; set; }
}
