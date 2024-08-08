using System.ComponentModel.DataAnnotations.Schema;

namespace DA.Kochbuch.Model.Authorization
{
	/// <ChangeLog>
	/// <Create Datum="28.07.2024" Entwickler="DA" />
	/// </ChangeLog>
	/// <summary>
	/// Class for dealing with access tokens for Rest-API
	/// </summary>
	public class AccessToken : BaseModel
	{
		public new Guid ID { get; set; }

		/// <summary>
		/// Gültigkeitsdauer des Tokens
		/// </summary>
		[NotMapped]
		public TimeSpan Lifetime { get; private set; }

		/// <summary>
		/// Determines whether a token is still valid
		/// </summary>
		public bool IsValid => (ChangeDate + Lifetime) > DateTime.UtcNow;
		public AccessToken()
		{
			ID = Guid.NewGuid();
			Lifetime = TimeSpan.FromMinutes(30);	// TODO AD: get this from cfg
		}
	}
}