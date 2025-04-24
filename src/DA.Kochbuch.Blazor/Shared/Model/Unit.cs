using System;
using System.Collections.Generic;

namespace DA.Kochbuch.Blazor.Server.Model;

public partial class Unit
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? ChangeDate { get; set; }

    public DateTime? CreationDate { get; set; }

    public bool Deleted { get; set; }

    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}
