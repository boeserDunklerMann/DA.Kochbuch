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

			MainPage = new AppShell();
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