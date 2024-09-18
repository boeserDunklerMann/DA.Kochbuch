using DA.Kochbuch.App.MVVM;

namespace DA.Kochbuch.App
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			Model.Recipe? selectedRecipe = ((MainVM)(BindingContext)).SelectedRecipe;
			Application.Current.MainPage = new NavigationPage(new RecipePage(selectedRecipe));
        }
    }
}
