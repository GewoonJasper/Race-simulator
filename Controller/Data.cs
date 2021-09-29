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
            AddParticipants();
            AddTracks();
        }

        public static void AddParticipants()
        {
            Driver driver1 = new Driver("Verstappen", 0, IParticipant.TeamColors.Blue);
            Driver driver2 = new Driver("Hamilton", 0, IParticipant.TeamColors.Grey);
            Driver driver3 = new Driver("Norris", 0, IParticipant.TeamColors.Yellow);
            Driver driver4 = new Driver("Sainz", 0, IParticipant.TeamColors.Red);

            competition.Participants.Add(driver1);
            competition.Participants.Add(driver2);
            competition.Participants.Add(driver3);
            competition.Participants.Add(driver4);
        }

        public static void AddTracks()
        {

            SectionTypes[] baan1 = new SectionTypes[] { SectionTypes.StartGrid,
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
                                                        SectionTypes.Straight,
                                                        SectionTypes.Straight,
                                                        SectionTypes.RightCorner,
                                                        SectionTypes.LeftCorner,
                                                        SectionTypes.RightCorner,
                                                        SectionTypes.Straight,
                                                        SectionTypes.RightCorner 
                                                      };
            Track track1 = new Track("EpicTrack", baan1);

            SectionTypes[] baan2 = new SectionTypes[] { SectionTypes.StartGrid,
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
                                                      };
            Track track2 = new Track("Bridges", baan2);

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
