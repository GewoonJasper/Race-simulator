using Model;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

namespace WpfApp {
    public partial class StatsRace : Window
    {
        private GameData _gameData;
        private List<IParticipant> _raceLeaderboard;

        public StatsRace()
        {
            InitializeComponent();

            _gameData = (GameData)GameGrid.DataContext;

            RefreshComponents();
        }

        public void RefreshComponents()
        {
            _raceLeaderboard = _gameData.RaceLeaderboard;

            RaceLeaderboardDrivers.Items.Clear();
            RaceLeaderboardDriversLaps.Items.Clear();
            RaceLeaderboardCars.Items.Clear();
            RaceLeaderboardNumbers.Items.Clear();
            RaceLeaderboardDriversPace.Items.Clear();
            RaceLeaderboardDriversQuality.Items.Clear();

            TrackName.Content = _gameData.TrackName;
            MaxLaps.Content = _gameData.MaxLaps;

            int x = 1;

            foreach (IParticipant participant in _raceLeaderboard)
            {
                RaceLeaderboardNumbers.Items.Add(x);
                x++;

                RaceLeaderboardDrivers.Items.Add(participant.Name);
                RaceLeaderboardDriversLaps.Items.Add(participant.Laps);
                RaceLeaderboardDriversPace.Items.Add(participant.Car.Speed * participant.Car.Performance);
                RaceLeaderboardDriversQuality.Items.Add(participant.Car.Quality);

                ImageSource imgSource = ConvertStringToImagesource(WPFVisualisation.getCar(participant, true));
                RaceLeaderboardCars.Items.Add(new Image() { Source = imgSource, Height = 30, Width = 50 });
            }
        }

        // van internet
        private ImageSource ConvertStringToImagesource(string img)
        {
            var image = new Bitmap(img);
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
    }
}
