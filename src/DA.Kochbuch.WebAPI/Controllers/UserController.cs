using DA.Kochbuch.Model;
using DA.Kochbuch.Model.Authorization;
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
		[HttpPost]
#pragma warning disable CS8600 // KochbuchContext is checked in VerifyAccessToken
#pragma warning disable CS8602 // KochbuchContext is checked in VerifyAccessToken
#pragma warning disable CS8603 // KochbuchContext is checked in VerifyAccessToken
		public async Task<IActionResult> CreateUserAsync(string username, string password, User newUser)
		{
			Logger.LogInformation($"running {nameof(CreateUserAsync)}");
			await VerifyUserAsync(username, password, true);

			await KochbuchContext.Users.AddAsync(newUser);
			await KochbuchContext.SaveChangesAsync();
			return Ok(newUser);
		}

		[HttpGet]
		//[Route(nameof(User))]
		public async Task<IEnumerable<User>> GetAllUsersAsync(string username, string password)
		{
			Logger.LogInformation($"running {nameof(GetAllUsersAsync)}");
			await VerifyUserAsync(username, password, true);
			// TODO DA: unschöner workaround
			KochbuchContext.Recipes.ToList();

			return await KochbuchContext.Users.Where(u => !u.Deleted).ToListAsync();
		}

		[HttpGet]
		[Route("{UserID}/{username}/{password}")]
		public async Task<User> GetUserAsync(string username, string password, int UserID)
		{
			Logger.LogInformation($"running {nameof(GetUserAsync)}");
			await VerifyUserAsync(username, password, true);


			return await KochbuchContext.Users.FirstOrDefaultAsync(u => !u.Deleted && u.ID == UserID);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateUserAsync(string username, string password, User user)
		{
			Logger.LogInformation($"running {nameof(UpdateUserAsync)}");
			await VerifyUserAsync(username, password, true);

			User userFromDb = await KochbuchContext.Users.FirstOrDefaultAsync(u=>!u.Deleted && u.ID==user.ID);
			if (userFromDb == null)
				throw new Exceptions.ObjectNotFoundException(nameof(User), user.ID);
			userFromDb.Name = user.Name;
			userFromDb.ChangeDate = DateTime.UtcNow;
			//userFromDb.OwnRecipes = user.OwnRecipes;
			await KochbuchContext.SaveChangesAsync();
			return Ok(userFromDb);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteUserAsync(string username, string password, User user)
		{
			Logger.LogInformation($"running {nameof(DeleteUserAsync)}");
			await VerifyUserAsync(username, password, true);
			User userFromDb = await KochbuchContext.Users.FirstOrDefaultAsync(u => !u.Deleted && u.ID == user.ID);
			if (userFromDb == null)
				throw new Exceptions.ObjectNotFoundException(nameof(User), user.ID);
			userFromDb.ChangeDate= DateTime.UtcNow;
			userFromDb.Deleted = true;
			await KochbuchContext.SaveChangesAsync();
			return Ok(userFromDb);
		}

#pragma warning restore CS8600 // Dereference of a possibly null reference.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8603 // Dereference of a possibly null reference.
		#endregion
	}
}