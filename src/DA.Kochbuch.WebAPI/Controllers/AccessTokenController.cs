using DA.Kochbuch.Model;
using DA.Kochbuch.Model.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DA.Kochbuch.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	/// <ChangeLog>
	/// <Create Datum="29.07.2024" Entwickler="DA" />
	/// </ChangeLog>
	/// <summary>
	/// Rest-API-Controller for access tokens
	/// </summary>
	public class AccessTokenController : KochbuchBaseController
	{
		public AccessTokenController(ILogger<AccessTokenController> logger, IConfiguration cfg) : base(logger, cfg)
		{
		}

		#region CRUD-ops
		[HttpPost]
		public async Task<AccessToken> CreateTokenAsync(string username, string password)
		{
			Logger.LogInformation($"running {nameof(CreateTokenAsync)}");
			if (KochbuchContext == null)
				throw new NullReferenceException(nameof(KochbuchContext));

			AccessToken token = BaseModel.Create<AccessToken>($"AT_{username}");
			KochbuchContext.AccessTokens.Add(token);
			await KochbuchContext.SaveChangesAsync();
			return token;
		}

		[HttpGet]
		public async Task<IEnumerable<AccessToken>> GetAllTokensAsync()
		{
			Logger.LogInformation($"running {nameof(GetAllTokensAsync)}");
			if (KochbuchContext == null)
				throw new NullReferenceException(nameof(KochbuchContext));
			return await KochbuchContext.AccessTokens.Where(t=>!t.Deleted).ToListAsync();
		}

		[HttpPut]
		public async Task UpdateTokenAsync(AccessToken token)
		{
			Logger.LogInformation($"running {nameof(UpdateTokenAsync)}");
			if (KochbuchContext == null)
				throw new NullReferenceException(nameof(KochbuchContext));
			AccessToken? tokenDb = await KochbuchContext.AccessTokens
				.FirstOrDefaultAsync(t => t.ID == token.ID && !t.Deleted);
			if (tokenDb == null)
				throw new Exceptions.ObjectNotFoundException(nameof(AccessToken), token.ID);
			tokenDb.ChangeDate = DateTime.UtcNow;
			await KochbuchContext.SaveChangesAsync();
		}

		[HttpDelete]
		public async Task DeleteTokenAsync(AccessToken token)
		{
			Logger.LogInformation($"running {nameof(DeleteTokenAsync)}");
			await DeleteTokenByIDAsync(token.ID);
		}

		[HttpDelete]
		[Route("{accessTokenID}")]
		public async Task<IActionResult> DeleteTokenByIDAsync(Guid accessTokenID)
		{
			Logger.LogInformation($"running {nameof(DeleteTokenByIDAsync)}");
			CheckContext();
			AccessToken? tokenDb = await KochbuchContext.AccessTokens
				.FirstOrDefaultAsync(t => t.ID.Equals(accessTokenID) && !t.Deleted);
			if (tokenDb == null)
				throw new Exceptions.ObjectNotFoundException(nameof(AccessToken), accessTokenID);
			tokenDb.Deleted = true;
			tokenDb.ChangeDate = DateTime.UtcNow;
			await KochbuchContext.SaveChangesAsync();
			return Ok(tokenDb);
		}
		#endregion
	}
}