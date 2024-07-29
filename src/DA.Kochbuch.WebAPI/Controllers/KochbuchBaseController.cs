using DA.Kochbuch.Model;
using Microsoft.AspNetCore.Mvc;

namespace DA.Kochbuch.WebAPI.Controllers
{
	/// <ChangeLog>
	/// <Create Datum="29.07.2024" Entwickler="DA" />
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