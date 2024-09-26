using DA.Kochbuch.App.Authorization;
using DA.Kochbuch.App.MVVM;

namespace DA.Kochbuch.App
{
    /// <ChangeLog>
    /// <Create Datum="??.09.2024" Entwickler="DA" />
    /// <Change Datum="26.09.2024" Entwickler="DA">btnLogin_Clicked added</Change>
    /// </ChangeLog>
    public partial class MainPage : ContentPage
	{
		private GoogleUser? googleUser;
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

		private async void btnLogin_Clicked(object sender, EventArgs e)
		{
			using (Authorization.Google auth = new Authorization.Google("371013138451-6uc53r25qa6mgjm98sea4rp25p3eovum.apps.googleusercontent.com", "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"))	// TODO DA: from cfg!
			{
                WebView signInWebView = new WebView
                {
					Source = auth.ConstructGoogleSignInUrl(),
					VerticalOptions = LayoutOptions.Fill
				};
				// Dirty workaround: "google in mobile apps doesn't allow auth2.0 authentication via webview for security reasons":
				// https://stackoverflow.com/a/69342626/12445867
				signInWebView.UserAgent = $"Mozilla/5.0 (Linux; Android {DeviceInfo.Current.Version.Major}.{DeviceInfo.Current.Version.Minor}; Pixel 2 Build/OPD3.170816.012) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.82 Mobile Safari/537.36";

                Grid grid = new Grid();
				grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star }); // row for webview
				grid.SetRow(signInWebView, 0);
				grid.Children.Add(signInWebView);
                ContentPage signInContentPage = new ContentPage
                {
					Content = grid,
				};
				await Navigation.PushModalAsync(signInContentPage);
				signInWebView.Navigating += async (sender, e) =>
				{
					string? code = auth.OnWebViewNavigating(e, signInContentPage);
					if (e.Url.StartsWith("http://localhost") && code != null)
					{
						using Authorization.Google authorize = new Authorization.Google("371013138451-6uc53r25qa6mgjm98sea4rp25p3eovum.apps.googleusercontent.com", "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");	// TODO DA: from cfg!
						(string? access_token, string? refresh_token) = await authorize.ExchangeCodeForAccessTokenAsync(code);
						if (access_token != null && refresh_token != null)
						{
							googleUser = await authorize.GetUsersDetailsAsync(access_token);
						}
					}
				};
			}
		}
	}
}
