﻿using System;
using Model;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; private set; }
        public static Race CurrentRace { get; private set; }
        public static EventHandler RaceChanged { get; set; }

        // Initialiseert de klasse Data, door drivers en tracks aan te maken en aan de competitie toe te voegen
        public static void Initialize (Driver[] drivers, Track[] tracks)
        {
            Competition = new Competition();

            AddParticipants(drivers, Competition);
            AddTracks(tracks, Competition);
        }

        // Voegt drivers toe aan de competitie
        public static void AddParticipants(Driver[] drivers, Competition competition)
        {
            if (drivers != null && drivers.Length != 0)
            {
                foreach (Driver driver in drivers)
                {
                    competition.Participants.Add(driver);
                }
            }
        }

        // Voegt tracks toe aan de competitie
        public static void AddTracks(Track[] tracks, Competition competition)
        {
            if (tracks != null && tracks.Length != 0)
            {
                foreach (Track track in tracks)
                {
                    competition.Tracks.Enqueue(track);
                }
            }
        }

        //Gaat naar de volgende baan zolang er nog banen in de queue zitten
        public static void NextRace(Competition competition)
        {
            Track track = competition.NextTrack();

            if (track != null)
            {
                CurrentRace = new Race(track, competition.Participants);
                RaceChanged(CurrentRace, new EventArgs());
            }
            else
            {
                CurrentRace = null;
            }
        }
    }
}
