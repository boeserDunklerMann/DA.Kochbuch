# App.xaml
merged die /Resources/Styles /.. Files in ein resourceDictionary

# App.xaml.cs
- Erstellt die Anwendung zur Laufzeit
- ctor erstellt ein erstes Fenster und weist dies der Property MainPage zu
	- diese bestimmt, welche Seite zum App-Start angezeigt wird
	- man kann plattformneutrale Anwendungslebenszyklus-Eventhandler überschreiben, die a wären:
		- OnStart
		- OnResume
		- OnSleep
	- definiert sind die in der Application-baseclass
	- 