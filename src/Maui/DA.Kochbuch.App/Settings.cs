using Newtonsoft.Json;
using System.IO;

namespace DA.Kochbuch.App
{
	/// <ChangeLog>
	/// <Create Datum="27.09.2024" Entwickler="DA" />
	/// </ChangeLog>
	public class Settings
	{
		const string _settingsfile = "appsettings.json";
		

		public static Settings Create()
		{
			// https://montemagno.com/dotnet-maui-appsettings-json-configuration/
			string settingsJson = File.ReadAllText(_settingsfile);
			return JsonConvert.DeserializeObject(settingsJson, typeof(Settings)) as Settings;
		}
		public string? ApiUser { get; set; }
        public string? ApiPass { get; set; }
        public string? ApiUrl { get; set; }
    }
}
