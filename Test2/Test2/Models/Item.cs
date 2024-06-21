using System.ComponentModel.DataAnnotations;

namespace Test2.Models;

public class Item
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)] 
    public string Name { get; set; } = string.Empty;
    
    public int Weight { get; set; }

    public ICollection<Backpack> Backpacks { get; set; } = new HashSet<Backpack>();
}