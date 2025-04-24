using System;
using System.Collections.Generic;

namespace DA.Kochbuch.Blazor.Server.Model;

public partial class RecipeImage
{
    public int Id { get; set; }

    public byte[]? Image { get; set; }

    public int? RecipeId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? ChangeDate { get; set; }

    public DateTime? CreationDate { get; set; }

    public bool Deleted { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
