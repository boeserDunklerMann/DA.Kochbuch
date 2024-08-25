using DA.Kochbuch.ApiClient;
using DA.Kochbuch.Model;

namespace DA.Kochbuch.ApiClient.Cons.Test
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Program p = new Program();
			p.Run();
		}

		void Run()
		{
			using (HttpClient http = new HttpClient())
			{
				Client client = new Client("http://localhost:5215/", http);
				{
					Guid token = CreateToken(client);
					ICollection<Recipe> recipes = client.RecipeAllAsync(token).Result;
				}
			}
		}

		Guid CreateToken(Client client)
		{
			var token = client.AccessTokenPOSTAsync("aa", "bc").Result;
			return token.ID;
		}
	}
}
