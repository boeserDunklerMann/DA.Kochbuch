using DA.Kochbuch.Model.UnitsTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DA.Kochbuch.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	/// <ChangeLog>
	/// <Create Datum="08.08.2024" Entwickler="DA" />
	/// </ChangeLog>
	public class UnitsTypesController(ILogger<UnitsTypesController> logger, IConfiguration cfg) : KochbuchBaseController(logger, cfg)
	{
#pragma warning disable CS8600 // Dereference of a possibly null reference.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8603 // Dereference of a possibly null reference.
		#region IngredientUnit
		[HttpGet]
		[Route(nameof(IngredientUnit))]
		public async Task<IEnumerable<IngredientUnit>> GetIngredientUnitsAsync(string username, string password)
		{
			Logger.LogInformation($"running {nameof(GetIngredientUnitsAsync)}");
			await VerifyUserAsync(username, password, true);
			return await KochbuchContext.Units.Where(ut => !ut.Deleted).ToListAsync();
		}
		#endregion
#pragma warning restore CS8600 // Dereference of a possibly null reference.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8603 // Dereference of a possibly null reference.
	}
}