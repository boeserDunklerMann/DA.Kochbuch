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
	/// </ChangeLog>
	public class BaseViewModel : INotifyPropertyChanged, IDisposable
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		
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
		}
	}
}
