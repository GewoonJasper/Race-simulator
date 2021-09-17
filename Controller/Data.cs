using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using SectionTypes = Model.Section.SectionTypes;

namespace Controller
{
    public static class Data
    {
        public static Competition competition { get; set; }
        public static Race CurrentRace { get; set; }

        public static void Initialize ()
        {
            competition = new Competition();
            addParticipants();
            addTracks();
        }

        public static void addParticipants()
        {
            Driver driver1 = new Driver();
            Driver driver2 = new Driver();
            Driver driver3 = new Driver();
            Driver driver4 = new Driver();

            competition.Participants = new List<IParticipant>();

            competition.Participants.Add(driver1);
            competition.Participants.Add(driver2);
            competition.Participants.Add(driver3);
            competition.Participants.Add(driver4);
        }

        public static void addTracks()
        {
            SectionTypes[] baan = new SectionTypes[7];
            baan[0] = SectionTypes.StartGrid;
            baan[1] = SectionTypes.Finish;
            baan[2] = SectionTypes.RightCorner;
            baan[3] = SectionTypes.RightCorner;
            baan[4] = SectionTypes.Straight;
            baan[5] = SectionTypes.RightCorner;
            baan[6] = SectionTypes.RightCorner;
            Track track1 = new Track("RightOval", baan);

            SectionTypes[] baan2 = new SectionTypes[7];
            baan2[0] = SectionTypes.StartGrid;
            baan2[1] = SectionTypes.Finish;
            baan2[2] = SectionTypes.LeftCorner;
            baan2[3] = SectionTypes.LeftCorner;
            baan2[4] = SectionTypes.Straight;
            baan2[5] = SectionTypes.LeftCorner;
            baan2[6] = SectionTypes.LeftCorner;
            Track track2 = new Track("LeftOval", baan2);

            competition.Tracks = new Queue<Track>();

            competition.Tracks.Enqueue(track1);
            competition.Tracks.Enqueue(track2);
        }

        public static void NextRace()
        {
            Track track = competition.NextTrack();

            if (track != null) {
                CurrentRace = new Race(track, competition.Participants);
            }
        }
    }
}
