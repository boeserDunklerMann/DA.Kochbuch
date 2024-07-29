using DA.Kochbuch.Model;
using DA.Kochbuch.Model.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DA.Kochbuch.WebAPI.Controllers
{
	[ApiController]
	[Route("Authorization")]
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
		[Route("AccessToken")]
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
		[Route("AccessToken")]
		public async Task<IEnumerable<AccessToken>> GetAllTokensAsync()
		{
			Logger.LogInformation($"running {nameof(GetAllTokensAsync)}");
			if (KochbuchContext == null)
				throw new NullReferenceException(nameof(KochbuchContext));
			return await KochbuchContext.AccessTokens.Where(t=>!t.Deleted).ToListAsync();
		}

		[HttpDelete]
		[Route("AccessToken")]
		public async Task DeleteTokenAsync(AccessToken token)
		{
			Logger.LogInformation($"running {nameof(DeleteTokenAsync)}");
			if (KochbuchContext == null)
				throw new NullReferenceException(nameof(KochbuchContext));
			AccessToken? tokenDb = await KochbuchContext.AccessTokens
				.FirstOrDefaultAsync(t=>t.ID == token.ID && !t.Deleted);
			if (tokenDb == null)
				throw new Exceptions.ObjectNotFoundException(nameof(AccessToken), token.ID);
			tokenDb.Deleted = true;
			tokenDb.ChangeDate = DateTime.UtcNow;
			await KochbuchContext.SaveChangesAsync();
		}

		[HttpPut]
		[Route(nameof(AccessToken))]
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
		#endregion
	}
}