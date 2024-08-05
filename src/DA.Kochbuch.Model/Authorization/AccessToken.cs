using System.ComponentModel.DataAnnotations.Schema;

namespace DA.Kochbuch.Model.Authorization
{
	/// <ChangeLog>
	/// <Create Datum="28.07.2024" Entwickler="DA" />
	/// </ChangeLog>
	/// <summary>
	/// Class for ddealing with access tokens for Rest-API
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
		public bool IsValid
		{
			get
			{
				//return (CreationDate+Lifetime)<DateTime.UtcNow; // TODO: das hier sauber machen. Mit LastLoginDate oder sowas
				return true;
			}
		}
		public AccessToken()
		{
			ID = Guid.NewGuid();
			Lifetime = TimeSpan.FromMinutes(30);	// TODO AD: get this from cfg
		}
	}
}