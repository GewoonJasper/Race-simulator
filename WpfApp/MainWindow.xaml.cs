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
        private GameData _gameData;

        public MainWindow()
        {
            InitializeComponent();

            _gameData = (GameData)GameGrid.DataContext;

            Driver driver1 = new Driver("Verstappen", 0, IParticipant.TeamColors.Blue);
            Driver driver2 = new Driver("Hamilton", 0, IParticipant.TeamColors.Grey);
            Driver driver3 = new Driver("Norris", 0, IParticipant.TeamColors.Yellow);
            Driver driver4 = new Driver("Sainz", 0, IParticipant.TeamColors.Red);
            Driver driver5 = new Driver("Perez", 0, IParticipant.TeamColors.Blue);
            Driver driver6 = new Driver("Vettel", 0, IParticipant.TeamColors.Green);

            Driver[] drivers = { driver1, driver2, driver3, driver4, driver5, driver6 };

            Track track1 = new Track("Oval", new SectionTypes[]
            {
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.Finish,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner
            });
            Track track2 = new Track("Epictrack", new SectionTypes[]
            {
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
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
                SectionTypes.RightCorner
            });
            Track track3 = new Track("Bridges", new SectionTypes[]
            {
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
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
                SectionTypes.RightCorner
            });
            Track track4 = new Track("Veldbaan", new SectionTypes[]
            {
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
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
                SectionTypes.RightCorner
            });

            Track[] tracks = { track1, track2, track3, track4 };

            Data.RaceChanged += OnRaceChanged;

            Data.Initialize(drivers, tracks);

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
        }
    }
}
