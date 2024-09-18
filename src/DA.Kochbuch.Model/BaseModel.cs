using System.ComponentModel.DataAnnotations.Schema;

namespace DA.Kochbuch.Model
{
	/// <ChangeLog>
	/// <Create Datum="24.07.2024" Entwickler="DA" />
	/// </ChangeLog>
	/// <remarks>
	/// all Dates are stored in Utc since we are an international app.
	/// </remarks>
	public abstract class BaseModel
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Änderungsdatum des Datensatzes
		/// </summary>
		public DateTime? ChangeDate { get; set; }
		/// <summary>
		/// Erstellungsdatum
		/// </summary>
		public DateTime? CreationDate { get; set; }
		public bool Deleted { get; set; }
		public override string ToString()
		{
			return Name;
		}
		public static T Create<T>(string name = "") where T : BaseModel, new()
		{
			return new T { Name = name, CreationDate = DateTime.UtcNow, ChangeDate = DateTime.UtcNow };
		}
	}
}
