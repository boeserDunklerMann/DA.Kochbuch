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
	/// </ChangeLog>
	public class RecipeVM : BaseViewModel, INotifyPropertyChanged, IDisposable
	{
		new public event PropertyChangedEventHandler? PropertyChanged;

		#region public exposed props
		public Recipe? SelectedRecipe
		{
			get;
			set;
		} = null;
		#endregion
		public RecipeVM() : base()
		{
		}
		public RecipeVM(Recipe selectedRecipe) : base()
		{
			SelectedRecipe = selectedRecipe;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedRecipe)));
		}
	}
}