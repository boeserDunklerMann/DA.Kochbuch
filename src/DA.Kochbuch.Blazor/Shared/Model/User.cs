using System;
using System.Collections.Generic;

namespace DA.Kochbuch.Blazor.Server.Model;

public partial class User
{
    public int Id { get; set; }

    public string GoogleId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime? ChangeDate { get; set; }

    public DateTime? CreationDate { get; set; }

    public bool Deleted { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
