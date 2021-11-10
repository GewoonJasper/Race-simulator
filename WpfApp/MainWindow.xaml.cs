using Controller;
using Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using static Model.Section;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StatsParticipants _participantsScreen;
        private StatsRace _raceScreen;
        private readonly GameData _gameData;

        public MainWindow()
        {
            InitializeComponent();

            _gameData = (GameData)GameGrid.DataContext;

            Data.RaceChanged += OnRaceChanged;

            Data.Initialize(new Competition(), GetDrivers(), GetTracks());

            Data.NextRace(Data.Competition);
        }

        public void OnDriversChanged(Object o, DriversChangedEventArgs e)
        {
            this.TrackImage.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    this.TrackImage.Source = null;
                    this.TrackImage.Source = WPFVisualisation.DrawTrack(e.Track);
                }));

            this.Dispatcher.Invoke(() =>
            {
                _gameData.RefreshScreens(_participantsScreen, _raceScreen);
            });
        }

        public void OnRaceChanged(Object o, EventArgs e)
        {
            RenderImage.ClearCache();
            Race r = (Race)o;
            r.DriversChanged += OnDriversChanged;
            r.DriversChanged += _gameData.OnPropertyChanged;
        }


        public static Driver[] GetDrivers()
        {
            Driver driver1 = new("Verstappen", 0, IParticipant.TeamColors.Blue);
            Driver driver2 = new("Hamilton", 0, IParticipant.TeamColors.Grey);
            Driver driver3 = new("Norris", 0, IParticipant.TeamColors.Yellow);
            Driver driver4 = new("Sainz", 0, IParticipant.TeamColors.Red);
            Driver driver5 = new("Perez", 0, IParticipant.TeamColors.Blue);
            Driver driver6 = new("Vettel", 0, IParticipant.TeamColors.Green);

            Driver[] drivers = { driver1, driver2, driver3, driver4, driver5, driver6 };

            return drivers;
        }

        public static Track[] GetTracks()
        {
            Track track1 = new("Oval", new SectionTypes[]
            {
                SectionTypes.Finish,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid
            }, 1);
            Track track2 = new("Epictrack", new SectionTypes[]
            {
                SectionTypes.Finish,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid
            }, 3);
            Track track3 = new("Bridges", new SectionTypes[]
            {
                SectionTypes.Finish,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid
            }, 1);
            Track track4 = new("Veldbaan", new SectionTypes[]
            {
                SectionTypes.Finish,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid
            }, 1);
            Track track5 = new("VerticalStart", new SectionTypes[]
            {
                SectionTypes.Finish,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid
            }, 2);

            Track[] tracks = { track1, track2, track3, track4, track5 };

            return tracks;
        }


        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        private void MenuItem_Race_Click(object sender, RoutedEventArgs e)
        {
            _raceScreen = new StatsRace();
            _raceScreen.Show();
        }

        private void MenuItem_Participants_Click(object sender, RoutedEventArgs e)
        {
            _participantsScreen = new StatsParticipants();
            _participantsScreen.Show();
        }

        private void MenuItem_PauzeStart_Click(object sender, RoutedEventArgs e)
        {
            if (Data.CurrentRace.IsGepauzeerd)
            {
                Data.CurrentRace.StartTimer();
            }
            else
            {
                Data.CurrentRace.StopTimer();
            }

            MenuItem item = (MenuItem)sender;
            item.Header = Data.CurrentRace?.GetPauseButton();
        }
    }
}
