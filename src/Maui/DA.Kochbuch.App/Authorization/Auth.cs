using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Security;
using System.Net;

namespace DA.Kochbuch.App.Authorization
{
    /// <ChangeLog>
    /// <Create Datum="26.09.2024" Entwickler="DA" />
    /// <Change Datum="26.09.2024" Entwickler="DA">class made non-static</Change>
    /// </ChangeLog>
    /// <remarks>
    /// https://github.com/JayMalli/Google_OAuth2.0-MAUI
    /// https://dev.to/jaymalli_programmer/google-oauth-20-authorization-service-implementation-in-net-maui-okl
    /// </remarks>
    public class Google(string clientID, string clientSecret) : IDisposable
    {
        private HttpClient? _httpClient = new HttpClient();

        public string ConstructGoogleSignInUrl()
        {
            // Specify the necessary parameters for the Google Sign-In URL
            string clientId = clientID;

            string responseType = "code";
            string accessType = "offline";
            string redirect_uri = "http://localhost:8080";
            string scope = "https://www.googleapis.com/auth/userinfo.profile";

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

        public string? OnWebViewNavigating(WebNavigatingEventArgs e, ContentPage signInContentPage)
        {
            if (e.Url.StartsWith("http://localhost"))
            {
                Uri uri = new Uri(e.Url);
                string query = WebUtility.UrlDecode(uri.Query);
                var queryParams = System.Web.HttpUtility.ParseQueryString(query);
                if (queryParams == null)
                {
                    throw new NullReferenceException(nameof(queryParams));
                }
                string? authorizationCode = queryParams.Get("code");

                signInContentPage.Navigation.PopModalAsync();
                return authorizationCode;
            }
            return null;
        }

        public async Task<(string?, string?)> ExchangeCodeForAccessTokenAsync(string code)
        {
            // Configure the necessary parameters for the token exchange
            //const string clientId = "371013138451-6uc53r25qa6mgjm98sea4rp25p3eovum.apps.googleusercontent.com";
            //const string client_secret = clientSecret; 
            const string redirectUri = "http://localhost:8080";

            // Construct the token exchange URL
            const string tokenUrl = "https://oauth2.googleapis.com/token";

            // Create a FormUrlEncodedContent object with the required parameters
            FormUrlEncodedContent content = new FormUrlEncodedContent(new Dictionary<string, string>
              {
                   { "code", code },
                   { "client_id", clientID },
                   { "client_secret", clientSecret },
                   { "redirect_uri", redirectUri },
                   { "grant_type", "authorization_code" }
              });

            // Send a POST request to the token endpoint to exchange the code for an access token
            if (_httpClient != null)
            {
                HttpResponseMessage response = await _httpClient.PostAsync(tokenUrl, content);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string responseContent = response.Content.ReadAsStringAsync().Result;

                    // Parse the JSON response to extract the tokens
                    JObject json = JObject.Parse(responseContent);
                    string? accessToken = json.GetValue("access_token")?.ToString();
                    string? refreshToken = json.GetValue("refresh_token")?.ToString();

                    return (accessToken, refreshToken);
                }
                else
                {
                    throw new ApplicationException($"Token exchange request failed with status code: {response.StatusCode}");
                }
            }
            else
                throw new NullReferenceException(nameof(_httpClient));
        }
        public async Task<GoogleUser?> GetUsersDetailsAsync(string accessToken)
        {
            // https://stackoverflow.com/a/7138474/12445867

            string url = $"https://www.googleapis.com/oauth2/v3/userinfo?alt=json&access_token={accessToken}";
            if (_httpClient != null)
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    JObject jsonObj = JObject.Parse(jsonString);
                    if (jsonObj != null)
                    {
                        var user = jsonObj.ToObject(typeof(GoogleUser));
                        return user as GoogleUser;
                    }
                    return null;
                }
				else
				{
					throw new ApplicationException($"Userinfo request failed with status code: {response.StatusCode}");
				}
			}
			else
                throw new NullReferenceException(nameof(_httpClient));
        }

        public void Dispose()
        {
            if (_httpClient != null)
            {
                _httpClient.Dispose();
                _httpClient = null;
            }
        }
    }
}