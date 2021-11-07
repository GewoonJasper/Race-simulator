using System;
using Controller;
using Model;
using NUnit.Framework;
using static Model.Section;

namespace ControllerTest
{
    [TestFixture]
    class Controller_Data_NextRaceShould
    {
        private Competition _competition;

        private Track track1;
        private Track track2;

        private Track[] tracks;

        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();

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
            });
            track2 = new Track("Epictrack", new SectionTypes[]
            {
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
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner
            });

            Data.RaceChanged += OnRaceChanged;
        }

        public void OnRaceChanged(object o, EventArgs e)
        {

        }

        [Test]
        public void NextRace_NoTracksInQueue_ReturnNull()
        {
            Data.NextRace(_competition);
            Assert.IsNull(Data.CurrentRace);
        }

        [Test]
        public void NextRace_OneTrackInQueue_ReturnFirst()
        {
            tracks = new Track[] { track1 };
            Data.AddTracks(tracks, _competition);
            
            Data.NextRace(_competition);
            Assert.AreEqual(track1, Data.CurrentRace.Track);
        }

        [Test]
        public void NextRace_OneTrackInQueue_SecondNull()
        {
            tracks = new Track[] { track1 };
            Data.AddTracks(tracks, _competition);

            Data.NextRace(_competition);
            Assert.AreEqual(track1, Data.CurrentRace.Track);

            Data.NextRace(_competition);
            Assert.IsNull(Data.CurrentRace);
        }

        [Test]
        public void NextRace_TwoTracksInQueue_ReturnFirst()
        {
            tracks = new Track[] { track1, track2 };
            Data.AddTracks(tracks, _competition);

            Data.NextRace(_competition);
            Assert.AreEqual(track1, Data.CurrentRace.Track);
        }

        [Test]
        public void NextRace_TwoTracksInQueue_ReturnSecond()
        {
            tracks = new Track[] { track1, track2 };
            Data.AddTracks(tracks, _competition);

            Data.NextRace(_competition);
            Assert.AreEqual(track1, Data.CurrentRace.Track);

            Data.NextRace(_competition);
            Assert.AreEqual(track2, Data.CurrentRace.Track);
        }

        [Test]
        public void NextRace_TwoTracksInQueue_ThirdNull()
        {
            tracks = new Track[] { track1, track2 };
            Data.AddTracks(tracks, _competition);

            Data.NextRace(_competition);
            Assert.AreEqual(track1, Data.CurrentRace.Track);

            Data.NextRace(_competition);
            Assert.AreEqual(track2, Data.CurrentRace.Track);

            Data.NextRace(_competition);
            Assert.IsNull(Data.CurrentRace);
        }
    }
}
