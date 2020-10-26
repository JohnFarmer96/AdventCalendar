// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VideoPage.cs" author="Jonathan Bauer >
// Project-Code: Copyright (c) 2020 - Jonathan Bauer. All Rights Reserved
// </copyright>
// <summary>
// Simple video player to embed videos in calender-application
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xam.Forms.VideoPlayer;

namespace Calendar
{
    /// <summary>
    /// Simple video player to embed videos in calender-application
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoPage : ContentPage
    {
        private const string GO_BACK = "Zurück zur Übersicht";

        /// <summary>
        /// Constructor of <see cref="VideoPage"/>
        /// </summary>
        /// <param name="URL"></param>
        public VideoPage(VideoSource URL)
        {
            InitializeComponent();

            MyVideoPlayer.Source = URL;

            Button ReturnButton = new Button()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                Text = GO_BACK,
                TextColor = Color.White,
                FontSize = 20,
                BackgroundColor = Color.Transparent,
            };

            ReturnButton.Clicked += ReturnToStartPage_Event;
            Grid.SetRow(ReturnButton, 0);
            Grid.SetColumn(ReturnButton, 0);
            MyVideoGrid.Children.Add(ReturnButton);
        }

        /// <summary>
        /// Handle return request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnToStartPage_Event(object sender, EventArgs e)
        {
            App.Current.MainPage = new OverviewPage();
        }

        /// <summary>
        /// Handle video completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VideoPlayer_PlayCompletion(object sender, EventArgs e)
        {
            App.Current.MainPage = new OverviewPage();
        }

        /// <summary>
        /// Handle video error
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VideoPlayer_PlayError(object sender, VideoPlayer.PlayErrorEventArgs e)
        {
            App.Current.MainPage = new OverviewPage();
        }
    }
}