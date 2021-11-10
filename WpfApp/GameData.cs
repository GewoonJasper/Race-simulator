using System;
using System.Collections.Generic;
using System.ComponentModel;
using Controller;
using Model;

namespace WpfApp
{
    public class GameData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string PauseButton => Data.CurrentRace?.GetPauseButton();
        public string TrackName => Data.CurrentRace?.Track.Name;
        public string MaxLaps => Data.CurrentRace?.MaxLaps.ToString();
        public List<IParticipant> RaceLeaderboard => Data.CurrentRace?.GetRaceLeaderboard();
        public List<IParticipant> CompetitionLeaderboard => Data.GetCompetitionLeaderboard(Data.Competition);

        public GameData(){}

        public void OnPropertyChanged(object o, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        public void RefreshScreens(StatsParticipants participantsScreen, StatsRace raceScreen)
        {
            participantsScreen?.RefreshComponents();
            raceScreen?.RefreshComponents();
        }
    }
}
