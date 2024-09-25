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

		#region public exposed props
		public Recipe? SelectedRecipe
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

		#region Ctors
		public RecipeVM() : base()
		{
		}
		public RecipeVM(Recipe selectedRecipe) : base()
		{
			SelectedRecipe = selectedRecipe;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedRecipe)));
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
	}
}