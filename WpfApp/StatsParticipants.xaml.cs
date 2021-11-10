using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Model;
using Image = System.Windows.Controls.Image;

namespace WpfApp {
    public partial class StatsParticipants : Window
    {
        private readonly GameData _gameData;
        private List<IParticipant> _competitionLeaderboard;

        public StatsParticipants()
        {
            InitializeComponent();

            _gameData = (GameData)GameGrid.DataContext;

            RefreshComponents();
        }

        public void RefreshComponents()
        {
            _competitionLeaderboard = _gameData.CompetitionLeaderboard;

            CompetitionLeaderboardDrivers.Items.Clear();
            CompetitionLeaderboardPoints.Items.Clear();
            CompetitionLeaderboardCars.Items.Clear();
            CompetitionLeaderboardNumbers.Items.Clear();

            int x = 1;

            foreach (IParticipant participant in _competitionLeaderboard)
            {
                CompetitionLeaderboardNumbers.Items.Add(x);
                x++;

                CompetitionLeaderboardDrivers.Items.Add(participant.Name);

                CompetitionLeaderboardPoints.Items.Add(participant.Points);

                ImageSource imgSource = ConvertStringToImagesource(WPFVisualisation.GetCar(participant, false));
                CompetitionLeaderboardCars.Items.Add(new Image() { Source = imgSource, Height = 30, Width = 50 });
            }
        }

        // van internet
        private static ImageSource ConvertStringToImagesource(string img)
        {
            var image = new Bitmap(img);
            using (MemoryStream ms = new())
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
