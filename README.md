# AdventCalendar
Simple Advent-Calendar application that will display videos stored in your Dropbox Account.

To customize the Source-Code you will need an access-token that is provided by Dropbox. This token needs to be added to Calendar/App.cs 

```
    /// <summary>
    /// Main application class.
    /// </summary>
    public partial class App : Application
    {
        // Modify Acces token with your specific Drobpox Token
        public static string AccessToken = "";
```

Background Images and Layout is fully customizable. 
Example GUI is shown below: 

<img src="https://github.com/JohnFarmer96/AdventCalendar/blob/master/AdventCalendar.png" width="40%">

&copy; Jonathan Bauer, 2020
