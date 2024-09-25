using DA.Kochbuch.Model;
using System.ComponentModel;

namespace DA.Kochbuch.App.MVVM
{
	/// <ChangeLog>
	/// <Create Datum="18.09.2024" Entwickler="DA" />
	/// <Change Datum="25.09.2024" Entwickler="DA">CanEdit added</Change>
	/// </ChangeLog>
	public class RecipeVM : BaseViewModel, INotifyPropertyChanged, IDisposable
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		/// <summary>
		/// The mode this VM was opened
		/// </summary>
		public enum EditMode
		{
			/// <summary>
			/// we edit an existing recipe
			/// </summary>
			Edit,
			/// <summary>
			/// we create a completly new recipe
			/// </summary>
			New,
			/// <summary>
			/// we are in read (view)-only mode
			/// </summary>
			None
		}

		#region public exposed props
		public Recipe? SelectedRecipe
		{
			get;
			set;
		} = null;
		public Ingredient? SelectedIngredient
		{
			get;
			set;
		} = null;

		/// <summary>
		/// Determines wheter the current user is allowed to modify this recipe
		/// </summary>
		public bool CanEdit
		{
			get
			{
				if (SelectedRecipe != null)
					return SelectedRecipe.User.Equals(CurrentUser);
				return false;
			}
		}
		#endregion

		#region private fields
		private EditMode editMode;
		#endregion
		#region Ctors
		public RecipeVM() : base()
		{
		}
		public RecipeVM(Recipe selectedRecipe, EditMode editMode) : base()
		{
			SelectedRecipe = selectedRecipe;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedRecipe)));
			this.editMode = editMode;
		}
		#endregion

		/// <summary>
		/// Loads the full recipe-data (including Ingredients and Images)
		/// </summary>
		public async Task LoadRecipeAsync()
		{
			if (SelectedRecipe != null && Username != null && Password != null)
			{
				if (api == null)
					throw new NullReferenceException(nameof(api));
				var recipe = await api.RecipeGETAsync(Username, Password, SelectedRecipe.ID);
				if (recipe != null)
				{
					recipe.User = SelectedRecipe.User;
					SelectedRecipe = recipe;
					//if (SelectedRecipe.Images.Count>0)
					//{
					//	ImageStream = new MemoryStream(SelectedRecipe.Images.ToArray()[0].Image);
					//	PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageStream)));
					//}
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedRecipe)));
				}
			}
		}

		#region Commands
		public DelegateCommand AddIngredientCommand => new DelegateCommand(AddIngredientAction);
		public DelegateCommand DelIngredientCommand => new DelegateCommand(DelIngredientAction);
		public DelegateCommand SaveCommand => new DelegateCommand(SaveAction);
		#endregion

		#region Actions
		private void AddIngredientAction()
		{
			if (SelectedRecipe != null)
			{
				var ingredient = BaseModel.Create<Ingredient>();
				SelectedIngredient = ingredient;
				SelectedRecipe.Ingredients.Add(ingredient);
			}
		}
		
		private void DelIngredientAction()
		{
			if (SelectedRecipe != null && SelectedIngredient != null)
			{
				SelectedRecipe.Ingredients.Remove(SelectedIngredient);
			}
		}

		private async void SaveAction()
		{
			if (api != null && SelectedRecipe != null)
				switch (editMode)
				{
					case EditMode.New:
						await api.RecipePOSTAsync(Username, Password, SelectedRecipe);
						break;
					case EditMode.Edit:
						SelectedRecipe.User.OwnRecipes.Clear();
						api.RecipePUTAsync(Username, Password, SelectedRecipe).Wait();	//TODO DA: HTTP 400
						break;
					default:
						throw new ApplicationException($"Cannot save Recipe when in not editable edit mode. Current edit mode: {editMode}");
				}
			else
				throw new NullReferenceException($"{nameof(api)} or {nameof(SelectedRecipe)}");
		}
		#endregion
	}
}