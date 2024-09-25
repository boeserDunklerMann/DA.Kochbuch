using DA.Kochbuch.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Kochbuch.App.MVVM
{
	/// <ChangeLog>
	/// <Create Datum="18.09.2024" Entwickler="DA" />
	/// <Change Datum="25.09.2024" Entwickler="DA">prop CurrentUser added</Change>
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

			CurrentUser = api.UserAllAsync(Username, Password).Result.First(u => u.Name.Equals("André", StringComparison.InvariantCultureIgnoreCase));
		}
	}
}