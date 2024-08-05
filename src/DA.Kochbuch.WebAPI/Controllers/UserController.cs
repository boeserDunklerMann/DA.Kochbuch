using DA.Kochbuch.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DA.Kochbuch.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	/// <ChangeLog>
	/// <Create Datum="05.08.2024" Entwickler="DA" />
	/// </ChangeLog>
	public class UserController(ILogger<UserController> logger, IConfiguration cfg) : KochbuchBaseController(logger, cfg)
	{
		#region CRUD ops
		[HttpGet]
		[Route(nameof(User))]
		public async Task<IEnumerable<User>> GetAllUsersAsync(Guid accessTokenID)
		{
			Logger.LogInformation($"running {nameof(GetAllUsersAsync)}");
			await VerifyAccessToken(accessTokenID, true);

			//if (KochbuchContext == null)
			//	throw new NullReferenceException(nameof(KochbuchContext));

#pragma warning disable CS8602 // KochbuchContext is checked in VerifyAccessToken
			return await KochbuchContext.Users.Where(u => !u.Deleted).ToListAsync();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
		}
		#endregion
	}
}