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
			api = new ApiClient.Client("http://localhost:5215/", http); // TODO DA: from cfg
			_units = new ObservableCollection<Model.UnitsTypes.IngredientUnit>();
			LoadDataAsync().Wait();
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
		private async Task<AccessToken> CreateAccessTokenAsync()
		{
			if (api == null)
				throw new NullReferenceException(nameof(api));
			return await api.AccessTokenPOSTAsync("ab", "cd");	// TODO DA: from cfg
		}

		private async Task LoadDataAsync()
		{
			AccessToken token = await CreateAccessTokenAsync();
			var units = await api.IngredientUnitAsync(token.ID);
			if (units != null)
			{
				_units.Clear();
				units.ToList().ForEach(u => _units.Add(u));
			}
		}
		#endregion

	}
}
