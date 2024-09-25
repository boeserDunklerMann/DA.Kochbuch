namespace DA.Kochbuch.App
{
	/// <ChangeLog>
	/// <Create Datum="11.09.2024" Entwickler="DA" />
	/// </ChangeLog>
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			//MainPage = new AppShell();
			var navPage = new NavigationPage(new MainPage());
			navPage.BarBackgroundColor = Colors.Brown;
			navPage.BarTextColor = Colors.White;

			MainPage = navPage;
		}

		/// <ChangeLog>
		/// <Create Datum="11.09.2024" Entwickler="DA" />
		/// </ChangeLog>
		protected override void OnStart()
		{
			base.OnStart();
		}
	}
}