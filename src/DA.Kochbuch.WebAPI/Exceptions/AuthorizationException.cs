namespace DA.Kochbuch.WebAPI.Exceptions
{
	/// <ChangeLog>
	/// <Create Datum="05.08.2024" Entwickler="DA" />
	/// </ChangeLog>
	public class AuthorizationException : Exception
	{
		public AuthorizationException(string msg) : base(msg)
		{
		}
	}
}