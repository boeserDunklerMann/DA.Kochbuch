# App.xaml
merged die `/Resources/Styles/..` Files in ein resourceDictionary

# App.xaml.cs
- Erstellt die Anwendung zur Laufzeit
- ctor erstellt ein erstes Fenster und weist dies der Property `MainPage` zu
	- diese bestimmt, welche Seite zum App-Start angezeigt wird
	- man kann plattformneutrale Anwendungslebenszyklus-Eventhandler überschreiben, die da wären:
		- `OnStart`
		- `OnResume`
		- `OnSleep`
	- definiert sind die in der `Application`-baseclass
# AppShell.xaml
- Ist die Hauptstruktur/`Shell` einer .NET MAUI App
- diese bietet Konfig.-Möglichkeiten bspw. für:
	- URI-based Navigation
	- Layout
	- Flyout-Navigation
	- Registerkarten für d. Stammverzeichnis der App
- die Standardvorlage stellt eine einzelne Seite (bzw. `ShellContent`) bereit, die beim App-Start vergrößert wird
# MainPage.xaml
- enthält die Definition der UI
- die Verwendung dieser XAML wird in `AppShell.xaml` bestimmt (Zeile 12)
# MainPage.xaml.cs
Der übliche WinForms-like-CodeBehind-Quatsch, welcher unbedingt vermieden werden soll.
# MauiProgram.cs
Der Einstiegspunkt einer MAUI App, dieser wird vom plattformspezifischen Code unter `Platforms/<PlatformName>/...` ziemlich am Ende aufgerufen. Dort wird die Methode `CreateMauiApp` aufgerufen.
