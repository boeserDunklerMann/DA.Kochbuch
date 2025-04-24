using System.ComponentModel.DataAnnotations;

namespace DA.Kochbuch.Blazor.Server.Model;

public partial class Recipe
{
    public int Id { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "This field accepts positive numbers only.")]
    public int NumberPersons { get; set; }

    public int UserId { get; set; }
    [Required]
    public string CookInstructon { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;

    public DateTime? ChangeDate { get; set; }

    public DateTime? CreationDate { get; set; }

    public bool Deleted { get; set; }

    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    public virtual ICollection<RecipeImage> RecipeImages { get; set; } = new List<RecipeImage>();

    public virtual User User { get; set; } = null!;
}