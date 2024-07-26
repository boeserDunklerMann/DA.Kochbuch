using DA.Kochbuch.Model;

namespace DA.Kochbuch.Cons.Test
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");
			using (var ctx = new Model.DatabaseContext())
			{
				User user = new User() { Name = "André", ChangeDate = DateTime.Now, ID = 1 };
				ctx.Users.Add(user);
				ctx.SaveChanges();
			}
		}
	}
}
