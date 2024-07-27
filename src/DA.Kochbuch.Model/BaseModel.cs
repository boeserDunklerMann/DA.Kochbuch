using System.ComponentModel.DataAnnotations.Schema;

namespace DA.Kochbuch.Model
{
	/// <ChangeLog>
	/// <Create Datum="24.07.2024" Entwickler="DA" />
	/// </ChangeLog>
	public abstract class BaseModel
	{
		// TODO AD: muss das hier alles virtual sein?

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		public virtual string Name { get; set; } = string.Empty;

		/// <summary>
		/// Änderungsdatum des Datensatzes
		/// </summary>
		public virtual DateTime? ChangeDate { get; set; }
		/// <summary>
		/// Erstellungsdatum
		/// </summary>
		public virtual DateTime? CreationDate { get; set; }
		public virtual bool Deleted { get; set; }
		//public abstract void PopulateMyID();
		public static T Create<T>(string name = null) where T : BaseModel, new()
		{
			return new T { Name = name };
		}
	}
}
