using DA.Kochbuch.App.MVVM;

namespace DA.Kochbuch.App;

public partial class RecipePage : ContentPage
{
	public RecipePage(Model.Recipe? recipe=null)
	{
		InitializeComponent();
		if (recipe != null)
		{
			this.BindingContext = new RecipeVM(recipe);
		}
	}

	private async void ContentPage_Loaded(object sender, EventArgs e)
	{
		await ((RecipeVM)BindingContext).LoadRecipeAsync();
    }
}