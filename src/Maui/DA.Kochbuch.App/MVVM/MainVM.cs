﻿using DA.Kochbuch.App.MVVM.Extension;
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
			if (api == null)
				throw new NullReferenceException(nameof(api));

			var allUsersWithRecipes = await api.UserAllAsync(Username, Password);

			// TODO DA: unschön, aber funktioniert
			allUsersWithRecipes.ToList().ForEach(u =>
			{
				if (u.OwnRecipes!=null && u.OwnRecipes.Any())
					u.OwnRecipes.ToList().ForEach(r => r.User = u);
			});

			if (allUsersWithRecipes != null && allUsersWithRecipes.Any())
			{
				_recipes.Clear();
				var usersRecipes = allUsersWithRecipes.Where(u=>u.OwnRecipes!=null).SelectMany(u => u.OwnRecipes);//.ToList();
				_recipes.AddRange(usersRecipes.ToList());
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
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