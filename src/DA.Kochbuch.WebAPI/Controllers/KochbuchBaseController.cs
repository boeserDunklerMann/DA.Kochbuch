using DA.Kochbuch.Model;
using DA.Kochbuch.Model.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DA.Kochbuch.WebAPI.Controllers
{
	/// <ChangeLog>
	/// <Create Datum="29.07.2024" Entwickler="DA" />
	/// <Change Datum="05.08.2024" Entwickler="DA">VerifyAccessToken added</Change>
	/// <Change Datum="05.08.2024" Entwickler="DA">CheckContext added</Change>
	/// </ChangeLog>
	/// <summary>
	/// base controller with DBContext
	/// </summary>
	public class KochbuchBaseController : ControllerBase, IDisposable
	{
		protected KochbuchContext? KochbuchContext { get; set; }
		protected ILogger Logger { get; set; }
		protected IConfiguration Configuration { get; set; }
		public KochbuchBaseController(ILogger logger, IConfiguration cfg)
		{
			if (cfg == null)
			{
				throw new NullReferenceException(nameof(cfg));
			}
			Logger = logger;
			Configuration = cfg;
			string connString = Configuration["ConnectionStrings:captainTrips"];
			if (string.IsNullOrEmpty(connString))
			{
				throw new ApplicationException("connectionstring not found");
			}
			KochbuchContext = new KochbuchContext(connString);
		}

		/// <ChangeLog>
		/// <Create Datum="05.08.2024" Entwickler="DA" />
		/// </ChangeLog>
		/// <summary>
		/// </summary>
		protected async Task<bool> VerifyAccessToken(Guid token, bool throwExceptionIfFailed)
		{
			CheckContext();
			AccessToken? accessToken = await KochbuchContext.AccessTokens.FirstAsync(at => !at.Deleted && at.ID.Equals(token));
			if (accessToken == null)
			{
				if (throwExceptionIfFailed)
				{
					throw new Exceptions.AuthorizationException($"Token {token.ToString()} could not be authorized.");
				}
				return false;
			}
			return accessToken.IsValid;
		}

		/// <ChangeLog>
		/// <Create Datum="05.08.2024" Entwickler="DA" />
		/// </ChangeLog>
		protected void CheckContext()
		{
			if (KochbuchContext == null)
				throw new NullReferenceException(nameof(KochbuchContext));
		}

		public void Dispose()
		{
			if (KochbuchContext != null)
			{
				KochbuchContext.Dispose();
				KochbuchContext = null;
			}
		}
	}
}