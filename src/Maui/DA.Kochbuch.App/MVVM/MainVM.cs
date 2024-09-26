using DA.Kochbuch.App.MVVM.Extension;
using DA.Kochbuch.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DA.Kochbuch.App.MVVM
{
	/// <ChangeLog>
	/// <Create Datum="26.08.2024" Entwickler="DA" />
	/// <Change Datum="18.09.2024" Entwickler="DA">Recipes added</Change>
	/// <Change Datum="18.09.2024" Entwickler="DA">AccessToken stuff removed</Change>
	/// </ChangeLog>
	/// https://learn.microsoft.com/de-de/dotnet/maui/xaml/fundamentals/data-binding-basics?view=net-maui-8.0
	public class MainVM : BaseViewModel, INotifyPropertyChanged, IDisposable
	{
		#region private fields
		private ObservableCollection<Model.UnitsTypes.IngredientUnit> _units;
		private ObservableCollection<Model.Recipe> _recipes;
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

		public ObservableCollection<Recipe> Recipes
		{
			get => _recipes;
			set
			{
				_recipes = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Recipes)));
			}
		}
		public Recipe? SelectedRecipe
		{
			get;
			set;
		} = null;
		#endregion

		public MainVM() : base()
		{
			_units = new ObservableCollection<Model.UnitsTypes.IngredientUnit>();
			_recipes = new ObservableCollection<Recipe>();
		}

		#region public methods
		public async Task LoadDataAsync()
		{
			if (Globals.GoogleUser != null)
			{
				if (api == null)
					throw new NullReferenceException(nameof(api));
				if (Username==null || Password==null)
				{
					throw new NullReferenceException($"need Username and Password for WebAPI");
				}

				var mySelf = await api.GoogleAsync(Username, Password, Globals.GoogleUser.sub);
				if (mySelf == null)
				{
					throw new ApplicationException($"GoogleID {Globals.GoogleUser.sub} not found!");
				}
				CurrentUser = mySelf;
				var myRecipes = await api.UserAsync(Username, Password, mySelf.ID);
				_recipes.Clear();
				_recipes.AddRange(myRecipes);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
			}
			else
            {
                // not logged in
            }
        }
		#endregion

		#region private methods
		private void LoadData()
		{
			LoadDataAsync().Wait();
		}
		#endregion

		#region Commands
		public DelegateCommand LoadDataCommand => new DelegateCommand(LoadData);
		#endregion
	}
}