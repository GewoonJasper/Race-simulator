using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace WpfApp {
    public partial class StatsParticipants : Window
    {
        private GameData _gameData;

        public StatsParticipants()
        {
            InitializeComponent();

            _gameData = (GameData)GameGrid.DataContext;

            RefreshComponents();
        }

        public void RefreshComponents()
        {
            DriversView.Items.Clear();
            DriversPointsView.Items.Clear();

            foreach (string name in _gameData.Drivers)
            {
                DriversView.Items.Add(name);
            }

            foreach (string points in _gameData.DriversPoints)
            {
                DriversPointsView.Items.Add(points);
            }
        }
    }
}
