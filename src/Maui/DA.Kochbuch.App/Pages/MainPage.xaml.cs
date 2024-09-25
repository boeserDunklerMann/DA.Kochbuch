using DA.Kochbuch.App.MVVM;

namespace DA.Kochbuch.App
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			Model.Recipe? selectedRecipe = ((MainVM)(BindingContext)).SelectedRecipe;
			await Navigation.PushAsync(new RecipeViewPage(selectedRecipe));
        }

		private async void ContentPage_Loaded(object sender, EventArgs e)
		{
			await ((MainVM)(BindingContext)).LoadDataAsync();
		}
	}
}
