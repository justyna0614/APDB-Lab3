using System.ComponentModel.DataAnnotations;

namespace WebApp.Animals;

public class AnimalDTO
{
    [Required, StringLength(100, MinimumLength = 3, ErrorMessage = "Nazwa musi być dluzsza niz 2 znaki i krotsza niz 100 znakow")]
    public string Name { get; set; }

    [Required, StringLength(10000, MinimumLength = 10, ErrorMessage = "Opis musi być dluzszy niz 9 znakow i krotszy niz 10000 znakow")]

    public string Description { get; set; }

    [Required, EnumDataType(typeof(Category))]
    public Category Category { get; set; }

    [Required, EnumDataType(typeof(Area))] public Area Area { get; set; }
}