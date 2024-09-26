using DA.Kochbuch.App.MVVM;

namespace DA.Kochbuch.App
{
	/// <ChangeLog>
	/// <Create Datum="??.09.2024" Entwickler="DA" />
	/// <Change Datum="26.09.2024" Entwickler="DA">btnLogin_Clicked added</Change>
	/// </ChangeLog>
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

		private void btnLogin_Clicked(object sender, EventArgs e)
		{
			WebView signInWebView = new WebView
			{
				Source = Auth.ConstructGoogleSignInUrl(),
				VerticalOptions = LayoutOptions.Fill
			};
			// Dirty workaround: "google in mobile apps doesn't allow auth2.0 authentication via webview for security reasons":
			// https://stackoverflow.com/a/69342626/12445867
			signInWebView.UserAgent = $"Mozilla/5.0 (Linux; Android {DeviceInfo.Current.Version.Major}.{DeviceInfo.Current.Version.Minor}; Pixel 2 Build/OPD3.170816.012) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.82 Mobile Safari/537.36";
			
			Grid grid = new Grid();
			grid.RowDefinitions.Add(new RowDefinition { Height= GridLength.Star }); // row for webview
			grid.SetRow(signInWebView, 0);
			grid.Children.Add(signInWebView);
			ContentPage signInContentPage = new ContentPage
			{
				Content = grid,
			};
			Application.Current.MainPage.Navigation.PushModalAsync(signInContentPage);
			signInWebView.Navigating += (sender, e) =>
			{
				string? code = Auth.OnWebViewNavigating(e, signInContentPage);
				if (e.Url.StartsWith("http://localhost") && code != null)
				{
					Console.WriteLine($"code: {code}");
					(string access_token, string refresh_token) = Auth.ExchangeCodeForAccessToken(code);
					if (access_token != null && refresh_token != null)
					{
						Console.WriteLine($"access_token:  {access_token}");
						Console.WriteLine($"refresh_token: {refresh_token}");
						Auth.GetUsersDetailsAsync(code);
						Auth.GetUsersDetailsAsync(access_token);
						Auth.GetUsersDetailsAsync(refresh_token);
					}
				}
			};
		}
	}
}
