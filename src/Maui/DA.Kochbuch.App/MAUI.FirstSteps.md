Für Details siehe [hier](https://learn.microsoft.com/de-de/training/modules/build-mobile-and-desktop-apps/3-create-a-maui-project-visual-studio)

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

Hier wird die App konfiguriert (bzgl. Fonts, DI, Services, etc.).
![MAUI Application startup flow](https://learn.microsoft.com/de-de/training/dot-net-maui/build-mobile-and-desktop-apps/media/3-startup-flow.png "MAUI Application startup flowchart")
# Projektressourcen
- schau dir die `.csproj` Datei mal genauer an:
	- unter `//Project/PropertyGroup/` sind die Zielplattform(frameworks) des Projekts definiert.
	- sowie das übliche (wie App-Titel, ID, Version, Supported-OS, etc.)
- im Ordner `Resources` kannst du beliebige Ressourcen hinzufügen, die die App benötigt
## Fonts
- hinzugefügte Fonts müssen in `CreateMauiApp` registriert werden.

# Erster Start unter Android-Emulator
Guck [hier mitte etwa](https://learn.microsoft.com/de-de/training/modules/build-mobile-and-desktop-apps/4-exercise-create-your-first-maui-app)