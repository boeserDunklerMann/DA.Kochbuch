
using DA.Kochbuch.Model;
using System.Collections.ObjectModel;

namespace DA.Kochbuch.App.MVVM.Extension
{
	/// <ChangeLog>
	/// <Create Datum="18.09.2024" Entwickler="DA" />
	/// </ChangeLog>
	internal static class ExtensionMethods
	{
		public static int WordCount(this string str)
		{
			return str.Split(new char[] {' ', '.', '?', '!'}, StringSplitOptions.RemoveEmptyEntries).Length;
		}
		// TODO DA: kann man das generischer machen?
		public static void AddRange(this ObservableCollection<Model.Recipe> collection, ICollection<Model.Recipe> collection2Add)
		{
            foreach (Recipe item in collection2Add)
            {
				collection.Add(item);
            }
        }
	}
}