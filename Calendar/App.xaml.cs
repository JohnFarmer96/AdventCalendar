// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.cs" author="Jonathan Bauer >
// Project-Code: Copyright (c) 2020 - Jonathan Bauer. All Rights Reserved
// </copyright>
// <summary>
// Main application class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Xamarin.Forms;
using Dropbox.Api;

namespace Calendar
{
    /// <summary>
    /// Main application class.
    /// </summary>
    public partial class App : Application
    {
        // Modify Acces token with your specific Drobpox Token
        public static string AccessToken = "";
        public static DropboxClient MyDropboxClient;
        public static Dropbox.Api.Users.FullAccount MyDropboxAccount;

        /// <summary>
        /// Constructor of <see cref="App"/>
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handle start procedures.
        /// </summary>
        protected override void OnStart()
        {
            MainPage = new OverviewPage();

            MyDropboxClient = new DropboxClient(AccessToken);
        }

        /// <summary>
        /// Handle sleep procedures.
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// Handle resume procedures.
        /// </summary>
        protected override void OnResume()
        {
            MainPage = new OverviewPage();
        }


    }
}
