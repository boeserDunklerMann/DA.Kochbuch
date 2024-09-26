﻿using Newtonsoft.Json.Linq;
using System.Net;

namespace DA.Kochbuch.App
{
	/// <ChangeLog>
	/// <Create Datum="26.09.2024" Entwickler="DA" />
	/// </ChangeLog>
	/// <remarks>
	/// https://github.com/JayMalli/Google_OAuth2.0-MAUI
	/// https://dev.to/jaymalli_programmer/google-oauth-20-authorization-service-implementation-in-net-maui-okl
	/// </remarks>
	public static class Auth
	{
		public static string ConstructGoogleSignInUrl()
		{
			// Specify the necessary parameters for the Google Sign-In URL
			const string clientId = "371013138451-6uc53r25qa6mgjm98sea4rp25p3eovum.apps.googleusercontent.com";

			const string responseType = "code";
			const string accessType = "offline";
			const string redirect_uri = "http://localhost:8080";
			const string scope = "https://www.googleapis.com/auth/drive";

			// Construct the Google Sign-In URL
			return "https://accounts.google.com/o/oauth2/v2/auth" +
							$"?client_id={Uri.EscapeDataString(clientId)}" +
							$"&redirect_uri={Uri.EscapeDataString(redirect_uri)}" +
							$"&response_type={Uri.EscapeDataString(responseType)}" +
							$"&scope={Uri.EscapeDataString(scope)}" +
							$"&access_type={Uri.EscapeDataString(accessType)}" +
							"&include_granted_scopes=true" +
							"&prompt=consent";
		}

		public static string? OnWebViewNavigating(WebNavigatingEventArgs e, ContentPage signInContentPage)
		{
			if (e.Url.StartsWith("http://localhost"))
			{
				Uri uri = new Uri(e.Url);
				string query = WebUtility.UrlDecode(uri.Query);
				var queryParams = System.Web.HttpUtility.ParseQueryString(query);
				string authorizationCode = queryParams.Get("code");

				signInContentPage.Navigation.PopModalAsync();
				return authorizationCode;
			}
			return null;
		}
		public static (string?, string?) ExchangeCodeForAccessToken(string code)
		{
			// Configure the necessary parameters for the token exchange
			const string clientId = "371013138451-6uc53r25qa6mgjm98sea4rp25p3eovum.apps.googleusercontent.com";
			const string clientSecret = "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";
			const string redirectUri = "http://localhost:8080";

			// Create an instance of HttpClient
			using (HttpClient client = new HttpClient())
			{
				// Construct the token exchange URL
				const string tokenUrl = "https://oauth2.googleapis.com/token";

				// Create a FormUrlEncodedContent object with the required parameters
				FormUrlEncodedContent content = new FormUrlEncodedContent(new Dictionary<string, string>
			  {
				   { "code", code },
				   { "client_id", clientId },
				   { "client_secret", clientSecret },
				   { "redirect_uri", redirectUri },
				   { "grant_type", "authorization_code" }
			  });

				// Send a POST request to the token endpoint to exchange the code for an access token
				HttpResponseMessage response = client.PostAsync(tokenUrl, content).Result;

				// Check if the request was successful
				if (response.IsSuccessStatusCode)
				{
					// Read the response content
					string responseContent = response.Content.ReadAsStringAsync().Result;

					// Parse the JSON response to extract the access token
					JObject json = JObject.Parse(responseContent);
					string accessToken = json.GetValue("access_token").ToString();
					string refreshToken = json.GetValue("refresh_token").ToString();
					return (accessToken, refreshToken);
				}

				else
				{
					// Exception:  "Token exchange request failed with status code: {response.StatusCode}"
				}
			}

			return (null, null);
		}
		public static async void GetUsersDetailsAsync(string accessToken)
		{
			string url = $"https://www.googleapis.com/oauth2/v3/userinfo?access_token={accessToken}";
			using(HttpClient client = new HttpClient())
			{
				HttpResponseMessage response = await client.GetAsync(url);
				if (response.IsSuccessStatusCode)
				{
					JObject jsonObj = JObject.Parse(await response.Content.ReadAsStringAsync());
					if (jsonObj != null)
					{

					}
				}
			}
		}
	}
}