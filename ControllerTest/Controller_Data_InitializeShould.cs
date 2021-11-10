using System;
using Controller;
using Model;
using NUnit.Framework;
using static Model.Section;

namespace ControllerTest
{
    [TestFixture]
    class Controller_Data_InitializeShould
    {
        private Competition competition;

        private Driver[] drivers;
        private Track[] tracks;

        private Driver driver1;
        private Driver driver2;
        private Driver driver3;
        private Driver driver4;

        private Track track1;
        private Track track2;
        private Track track3;

        [SetUp]
        public void SetUp()
        {
            competition = new Competition();

            driver1 = new Driver("Verstappen", 0, IParticipant.TeamColors.Blue);
            driver2 = new Driver("Hamilton", 0, IParticipant.TeamColors.Grey);
            driver3 = new Driver("Norris", 0, IParticipant.TeamColors.Yellow);
            driver4 = new Driver("Sainz", 0, IParticipant.TeamColors.Red);

            drivers = new Driver[] { driver1, driver2, driver3, driver4 };

            track1 = new Track("Oval", new SectionTypes[]
            {
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.Finish,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner
            }, 1);
            track2 = new Track("Epictrack", new SectionTypes[]
            {
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
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
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner
            }, 1);
            track3 = new Track("Bridges", new SectionTypes[]
            {
                SectionTypes.StartGrid,
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
            }, 1);

            tracks = new Track[] { track1, track2, track3 };
        }

        [Test]
        public void Initialize_AddNothing_ReturnEmpty()
        {
            Data.Initialize(competition, null, null);

            Assert.IsEmpty(Data.Competition.Participants);
            Assert.IsEmpty(Data.Competition.Tracks);
        }

        [Test]
        public void Initialize_AddEmptyTrack_ReturnEmpty()
        {
            Data.Initialize(competition, null, Array.Empty<Track>());

            Assert.IsEmpty(Data.Competition.Participants);
            Assert.IsEmpty(Data.Competition.Tracks);
        }

        [Test]
        public void Initialize_AddEmptyDriver_ReturnEmpty()
        {
            Data.Initialize(competition, Array.Empty<Driver>(), null);

            Assert.IsEmpty(Data.Competition.Participants);
            Assert.IsEmpty(Data.Competition.Tracks);
        }

        [Test]
        public void Initialize_AddEmptyBoth_ReturnEmpty()
        {
            Data.Initialize(competition, Array.Empty<Driver>(), Array.Empty<Track>());

            Assert.IsEmpty(Data.Competition.Participants);
            Assert.IsEmpty(Data.Competition.Tracks);
        }

        [Test]
        public void Initialize_AddOnlyTracks_ReturnOnlyTrack()
        {
            Data.Initialize(competition, null, tracks);

            Assert.IsEmpty(Data.Competition.Participants);
            Assert.AreEqual(tracks, Data.Competition.Tracks);
        }

        [Test]
        public void Initialize_AddOnlyDrivers_ReturnOnlyDrivers()
        {
            Data.Initialize(competition, drivers, null);

            Assert.IsEmpty(Data.Competition.Tracks);
            Assert.AreEqual(drivers, Data.Competition.Participants);
        }

        [Test]
        public void Initialize_AddBoth_ReturnBoth()
        {
            Data.Initialize(competition, drivers, tracks);

            Assert.AreEqual(tracks, Data.Competition.Tracks);
            Assert.AreEqual(drivers, Data.Competition.Participants);
        }
    }
}
