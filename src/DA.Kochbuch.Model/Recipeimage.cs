using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Kochbuch.Model
{
	/// <ChangeLog>
		/// <Create Datum="25.09.2024" Entwickler="DA" />
		/// </ChangeLog>
	public class Recipeimage: BaseModel
	{
        public byte[]? Image { get; set; }
    }
}
