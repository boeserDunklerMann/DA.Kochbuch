namespace DA.Kochbuch.App
{
	/// <ChangeLog>
	/// <Create Datum="26.09.2024" Entwickler="DA" />
	/// </ChangeLog>
	/// <summary>
	/// static class for storing some global (program-wide) informations
	/// </summary>
	public static class Globals
	{
		/// <summary>
		/// the logged-in Google user
		/// </summary>
		public static Authorization.GoogleUser? GoogleUser { get; set; } = null;
		/// <summary>
		/// the google-api's access token
		/// </summary>
		public static string? AccessToken { get; set; } = null;
		/// <summary>
		/// the google-api's refrash token
		/// </summary>
		public static string? RefreshToken { get; set; } = null;
    }
}