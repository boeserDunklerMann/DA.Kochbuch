using DA.Kochbuch.App.Authorization;
using DA.Kochbuch.Model;

namespace DA.Kochbuch.App.MVVM
{
	/// <ChangeLog>
	/// <Create Datum="18.09.2024" Entwickler="DA" />
	/// <Change Datum="25.09.2024" Entwickler="DA">prop CurrentUser added</Change>
	/// <Change Datum="27.09.2024" Entwickler="DA">prop GoogleUser added</Change>
	/// <Change Datum="27.09.2024" Entwickler="DA">prop LoggedIn added</Change>
	/// </ChangeLog>
	public class BaseViewModel : IDisposable
	{
		#region Fields
		protected HttpClient? http;
		protected ApiClient.Client? api;
		#endregion

		#region Public exposed props
		/// <summary>
		/// Credentials for accesssing WebAPI
		/// </summary>
		public string? Username
		{
			get;
			set;
		}
		/// <summary>
		/// Credentials for accesssing WebAPI
		/// </summary>
		public string? Password
		{
			get; set;
		}
		/// <summary>
		/// The user, who is currently logged in
		/// </summary>
		public User? CurrentUser { get; set; }
		/// <summary>
		/// The corresponding GoogleUser for <see cref="CurrentUser" />
		/// </summary>
		public GoogleUser? GoogleUser => Globals.GoogleUser;

		public bool LoggedIn => this.GoogleUser != null;
		#endregion

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public BaseViewModel()
		{
			http = new HttpClient();
			api = new ApiClient.Client("http://192.168.2.108:5002/", http); // TODO DA: from cfg
			Username = "ab";
			Password = "cd";

			//CurrentUser = api.UserAllAsync(Username, Password).Result.First(u => u.Name.Equals("André", StringComparison.InvariantCultureIgnoreCase));
		}
	}
}