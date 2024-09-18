using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA.Kochbuch.Model;
using DA.Kochbuch.Model.Authorization;

namespace DA.Kochbuch.App.MVVM
{
	/// <ChangeLog>
	/// <Create Datum="26.08.2024" Entwickler="DA" />
	/// </ChangeLog>
	/// https://learn.microsoft.com/de-de/dotnet/maui/xaml/fundamentals/data-binding-basics?view=net-maui-8.0
	public class MainVM : INotifyPropertyChanged, IDisposable
	{
		#region private fields
		private HttpClient? http;
		private ApiClient.Client? api;
		private ObservableCollection<Model.UnitsTypes.IngredientUnit> _units;
		private AccessToken? _token;
		#endregion

		public event PropertyChangedEventHandler? PropertyChanged;

		#region public exposed props
		public ObservableCollection<Model.UnitsTypes.IngredientUnit> Units
		{
			get => _units;
			set
			{
				_units = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Units)));
			}
		}
		#endregion

		public MainVM()
		{
			http = new HttpClient();
			api = new ApiClient.Client("http://192.168.2.108:5002/", http); // TODO DA: from cfg
			_units = new ObservableCollection<Model.UnitsTypes.IngredientUnit>();
			//LoadDataAsync();
		}

		public void Dispose()
		{
			if (http != null)
			{
				http.Dispose();
				http = null;
			}
		}

		#region private methods
		/// <summary>
		/// Returns a valid token
		/// </summary>
		/// <returns></returns>
		/// <exception cref="NullReferenceException">if we dont have an APIClient</exception>
		private async Task<AccessToken> GetAccessTokenAsync()
		{
			if (api == null)
				throw new NullReferenceException(nameof(api));
			if (_token != null && _token.IsValid)
				return _token;
			_token = await api.AccessTokenPOSTAsync("ab", "cd");  // TODO DA: from cfg
			return _token;
		}

		private async void LoadDataAsync()
		{
			AccessToken token = await GetAccessTokenAsync();
			var units = await api.IngredientUnitAsync(token.ID);
			if (units != null)
			{
				_units.Clear();
				units.ToList().ForEach(u => _units.Add(u));
			}
		}
		#endregion

		#region Commands
		public DelegateCommand LoadDataCommand => new DelegateCommand(LoadDataAsync);
		#endregion
	}
}