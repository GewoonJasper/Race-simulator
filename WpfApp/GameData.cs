using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using System.Windows.Markup;
using Controller;

namespace WpfApp
{
    public class GameData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string StartPauze
        {
            get => Data.CurrentRace?.StartPauzeer();
            set => Data.CurrentRace?.StartPauzeer(); //TODO pauzeer race veranderd niet in start race
        }
        
        public string TrackName
        {
            get => Data.CurrentRace?.Track.Name;
            set => TrackName = "";
        }

        //public List<string> drivers
        //{
        //    get => Data.Competition.Participants.;
        //    set => drivers = null;
        //}

        // TODO ipv 6 drivers een list aanmaken
        public string driver1
        {
            get => Data.Competition?.Participants[0].Name;
            set => driver1 = "";
        }
        public string driver2
        {
            get => Data.Competition?.Participants[1].Name;
            set => driver2 = "";
        }
        public string driver3
        {
            get => Data.Competition?.Participants[2].Name;
            set => driver3 = "";
        }
        public string driver4
        {
            get => Data.Competition?.Participants[3].Name;
            set => driver4 = "";
        }
        public string driver5
        {
            get => Data.Competition?.Participants[4].Name;
            set => driver5 = "";
        }
        public string driver6
        {
            get => Data.Competition?.Participants[5].Name;
            set => driver6 = "";
        }

        public string pointsDriver1
        {
            get => Data.Competition?.Participants[0].Points.ToString();
            set => pointsDriver1 = "";
        }
        public string pointsDriver2
        {
            get => Data.Competition?.Participants[1].Points.ToString();
            set => pointsDriver2 = "";
        }
        public string pointsDriver3
        {
            get => Data.Competition?.Participants[2].Points.ToString();
            set => pointsDriver3 = "";
        }
        public string pointsDriver4
        {
            get => Data.Competition?.Participants[3].Points.ToString();
            set => pointsDriver4 = "";
        }
        public string pointsDriver5
        {
            get => Data.Competition?.Participants[4].Points.ToString();
            set => pointsDriver5 = "";
        }
        public string pointsDriver6
        {
            get => Data.Competition?.Participants[5].Points.ToString();
            set => pointsDriver6 = "";
        }

        public string nextPoints
        {
            get => Data.CurrentRace?._points.ToString();
            set => nextPoints = "";
        }

        public GameData() { }

        public void OnPropertyChanged(object o, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}
