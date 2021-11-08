using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Model;
using Image = System.Windows.Controls.Image;

namespace WpfApp {
    public partial class StatsParticipants : Window
    {
        private GameData _gameData;
        private List<IParticipant> _drivers;

        public StatsParticipants()
        {
            InitializeComponent();

            _gameData = (GameData)GameGrid.DataContext;

            RefreshComponents();
        }

        public void RefreshComponents()
        {
            _drivers = _gameData.Drivers;

            DriversView.Items.Clear();
            DriversPointsView.Items.Clear();
            DriversCarsView.Items.Clear();

            foreach (IParticipant participant in _drivers)
            {
                DriversView.Items.Add(participant.Name);
                DriversPointsView.Items.Add(participant.Points);

                ImageSource imgSource = ConvertStringToImagesource(WPFVisualisation.getCar(participant, false));
                DriversCarsView.Items.Add(new Image() { Source = imgSource, Height = 30, Width = 50 });
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
