using DA.Kochbuch.App.MVVM;

namespace DA.Kochbuch.App.Pages;

public partial class RecipeEditPage : ContentPage
{
	/// <ChangeLog>
	/// <Create Datum="25.09.2024" Entwickler="DA" />
	/// </ChangeLog>
	public RecipeEditPage(Model.Recipe? recipe = null)
	{
		InitializeComponent();
		if (recipe != null)
		{
			this.BindingContext = new RecipeVM(recipe, RecipeVM.EditMode.Edit);
		}
	}
}