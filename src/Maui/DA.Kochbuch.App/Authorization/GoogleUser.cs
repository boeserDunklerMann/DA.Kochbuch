using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Kochbuch.App.Authorization
{
	/// <ChangeLog>
	/// <Create Datum="26.09.2024" Entwickler="DA" />
	/// </ChangeLog>
	public class GoogleUser
	{
		public string sub { get; set; }
		public string name { get; set; }
		public string given_name { get; set; }
		public string family_name { get; set; }
		public string picture { get; set; }
	}
}