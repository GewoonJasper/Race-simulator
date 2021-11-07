using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using System.Windows.Markup;
using Controller;
using Model;

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

        public List<string> Drivers
        {
            get
            {
                List<string> driverList = new List<string>();
                foreach (IParticipant participant in Data.Competition?.Participants)
                {
                    driverList.Add(participant.Name);
                }

                return driverList;
            }
            set
            {
                List<string> driverList = new List<string>();
                foreach (IParticipant participant in Data.Competition?.Participants)
                {
                    driverList.Add(participant.Name);
                }

                Drivers = driverList;
            }
        }//TODO linq maken ipv foreach

        public List<string> DriversPoints
        {
            get
            {
                List<string> driverList = new List<string>();
                foreach (IParticipant participant in Data.Competition?.Participants)
                {
                    driverList.Add(participant.Points.ToString());
                }

                return driverList;
            }
            set
            {
                List<string> driverList = new List<string>();
                foreach (IParticipant participant in Data.Competition?.Participants)
                {
                    driverList.Add(participant.Points.ToString());
                }

                DriversPoints = driverList;
            }
        }//TODO linq maken ipv foreach

        public GameData(){ }

        public void OnPropertyChanged(object o, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        public void RefreshScreens(StatsParticipants participantsScreen, StatsRace raceScreen)
        {
            participantsScreen?.RefreshComponents();
        }
    }
}
