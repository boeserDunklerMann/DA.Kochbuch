using DA.Kochbuch.App.MVVM;
using DA.Kochbuch.App.Pages;

namespace DA.Kochbuch.App;

public partial class RecipeViewPage : ContentPage
{
	public RecipeViewPage(Model.Recipe? recipe=null)
	{
		InitializeComponent();
		if (recipe != null)
		{
			this.BindingContext = new RecipeVM(recipe, RecipeVM.EditMode.None);
		}
	}

	private async void ContentPage_Loaded(object sender, EventArgs e)
	{
		await ((RecipeVM)BindingContext).LoadRecipeAsync();
    }

	private async void Button_Clicked(object sender, EventArgs e)
	{
		Model.Recipe? selectedRecipe = ((RecipeVM)(BindingContext)).SelectedRecipe;
		await Navigation.PushAsync(new RecipeEditPage(selectedRecipe));
	}
}