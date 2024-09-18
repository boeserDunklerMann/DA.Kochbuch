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
	}
}
