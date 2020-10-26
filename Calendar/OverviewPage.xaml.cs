// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OverviewPage.cs" author="Jonathan Bauer >
// Project-Code: Copyright (c) 2020 - Jonathan Bauer. All Rights Reserved
// </copyright>
// <summary>
// Calender overview page.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xam.Forms.VideoPlayer;
using System.Text;

namespace Calendar
{
    /// <summary>
    /// Calender overview page. 
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverviewPage : ContentPage
    {
        // const strings to ensure proper functionality
        private const string DOOR_BRIGHT = "Door_Bright.png";
        private const string DOOR_GOLD = "Door_Gold.png";
        private const string ID_KEY = "id";
        private const string IMAGE_FILEDIR = "Background/Background_";
        private const string IMAGE_SUFFIX = ".jpg";

        public readonly int[] Dates = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };

        public int CurrentDayIndex
        {
            get
            {
                return (int)DateTime.Now.Day;
            }
        }

        public int CurrentMonthIndex
        {
            get
            {
                return (int)DateTime.Now.Month;
            }
        }

        /// <summary>
        /// Constructor of <see cref="OverviewPage"/>
        /// </summary>
        public OverviewPage()
        {
            InitializeComponent();
            AssignButtons();
            AssignBackground();
        }

        /// <summary>
        /// Indicate if video got played. 
        /// </summary>
        /// <param name="Day"></param>
        /// <returns></returns>
        private bool GotPlayed(int Day)
        {
            bool videoPlayed = false;

            if(Application.Current.Properties.ContainsKey(ID_KEY))
            {
                if (int.TryParse(Application.Current.Properties[ID_KEY].ToString(), out int DayStamp))
                {
                    videoPlayed |= Day <= DayStamp;
                }
            }

            return videoPlayed;
        }

        /// <summary>
        /// Get background image of corresponding day.
        /// </summary>
        /// <returns></returns>
        private string GetTodaysImage(int dayIndex)
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(IMAGE_FILEDIR);
            strb.Append(dayIndex.ToString());
            strb.Append(IMAGE_SUFFIX);
            return strb.ToString();
        }

        /// <summary>
        /// Assign page background.
        /// </summary>
        public void AssignBackground()
        {
            int index;
            if (CurrentMonthIndex == 12 && CurrentDayIndex <= 24)
                index = CurrentDayIndex;
            else
                index = 1;

            MyBackgroundImage.Source = ImageSource.FromFile(GetTodaysImage(index));
        }

        /// <summary>
        /// Assign button layout.
        /// </summary>
        void AssignButtons()
        { 
            Random rnd = new Random();
            int[] RandomizedDates = Dates.OrderBy(x => rnd.Next()).ToArray();
            int DayIndex = 0;

            for (int row = 1; row < 7; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    Button button = new Button()
                    {
                        Text = RandomizedDates[DayIndex].ToString(),
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        FontSize = 40,
                        BackgroundColor = Color.Transparent,
                        FontFamily = Device.RuntimePlatform == Device.Android ? "beacon.ttf#beacon" :
                                        Device.RuntimePlatform == Device.iOS ? "beacon" :
                                        null
                    };

                    ImageButton DoorImage = new ImageButton()
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        BackgroundColor = Color.Transparent,
                    };

                    if (RandomizedDates[DayIndex] < CurrentDayIndex && CurrentMonthIndex == 12)
                    {
                        button.TextColor = Color.FromRgb(203,203,203);
                        DoorImage.Source = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile(DOOR_BRIGHT) :
                                            Device.RuntimePlatform == Device.iOS ? ImageSource.FromFile(DOOR_BRIGHT) :
                                            null;
                    }
                    else if (RandomizedDates[DayIndex] == CurrentDayIndex && CurrentMonthIndex == 12 && GotPlayed(RandomizedDates[DayIndex]))
                    {
                        button.TextColor = Color.FromRgb(203, 203, 203);
                        DoorImage.Source = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile(DOOR_BRIGHT) :
                                            Device.RuntimePlatform == Device.iOS ? ImageSource.FromFile(DOOR_BRIGHT) :
                                            null;
                    }
                    else
                    {
                        button.TextColor = Color.FromRgb(202,151,51);
                        DoorImage.Source = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile(DOOR_GOLD) :
                                            Device.RuntimePlatform == Device.iOS ? ImageSource.FromFile(DOOR_GOLD) :
                                            null;
                    }

                    DayIndex++;

                    Grid.SetRow(DoorImage, row);
                    Grid.SetColumn(DoorImage, column);
                    AdventGrid.Children.Add(DoorImage);

                    button.Clicked += Day_Event;
                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, column);
                    AdventGrid.Children.Add(button);
                }
            }
        }

        /// <summary>
        /// Get streamable video link from dropbox folder.
        /// </summary>
        /// <param name="Directory">Absolute dropbox directory path.</param>
        /// <returns></returns>
        private async Task<string> TaskStreamableLink(string Directory)
        {
            Dropbox.Api.Files.GetTemporaryLinkResult MyLinkResult = await App.MyDropboxClient.Files.GetTemporaryLinkAsync(Directory);
            string StreamableLink = MyLinkResult.Link;
            return StreamableLink;
        }

        /// <summary>
        /// Get streamable video link from dropbox folder.
        /// </summary>
        /// <param name="Directory">Absolute dropbox directory path.</param>
        /// <returns></returns>
        public string GetStreamableLink(string Directory)
        {
            Task<string> GetLinkTask = Task.Run(() => TaskStreamableLink(Directory));
            GetLinkTask.Wait();
            string StreamableLink = GetLinkTask.Result;
            return StreamableLink;
        }

        /// <summary>
        /// Handle click on particular day.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Day_Event(object sender, EventArgs e)
        {
            Button ClickedButton = (Button)sender;
            int.TryParse(ClickedButton.Text, out int content);

            string Directory;

            if (content <= CurrentDayIndex && CurrentMonthIndex == 12)
            {
                Directory = "/Adventskalender/Tag_" + content.ToString() + ".mp4";

                if(Application.Current.Properties.ContainsKey("id"))
                {
                    if (int.TryParse(Application.Current.Properties["id"].ToString(), out int DayStamp))
                    {
                        if (content > DayStamp)
                            Application.Current.Properties["id"] = content.ToString();
                    }
                }
                else
                {
                    Application.Current.Properties["id"] = content.ToString();
                }

                string StreamableLink = GetStreamableLink(Directory);

                VideoSource MyVideoSource = new UriVideoSource() { Uri = StreamableLink };
                App.Current.MainPage = new VideoPage(MyVideoSource);
            }
            else
            {
                DisplayAlert("Caught You!", "Don't Cheat!", "I'm sorry...");
            }            
        }
    }
}