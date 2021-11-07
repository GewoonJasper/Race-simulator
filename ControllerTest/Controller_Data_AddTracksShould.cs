using System;
using Model;
using NUnit.Framework;
using static Model.Section;

namespace ControllerTest
{
    [TestFixture]
    class Controller_Data_AddTracksShould
    {
        private Competition _competition;

        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
        }

        [Test]
        public void AddTracks_EmptyQueue_ReturnNull()
        {
            Track[] tracks = Array.Empty<Track>();
            Controller.Data.AddTracks(tracks, _competition);
            Assert.IsNull(_competition.NextTrack());
        }

        [Test]
        public void AddTracks_OneInQueue_ReturnTrack()
        {
            Track track1 = new Track("Oval", new SectionTypes[]
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
            Track[] tracks = new Track[] { track1 };
            Controller.Data.AddTracks(tracks, _competition);
            Assert.AreEqual(tracks, _competition.Tracks);
        }

        [Test]
        public void AddTracks_MoreInQueue_ReturnTracks()
        {
            Track track1 = new Track("Oval", new SectionTypes[]
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
            Track track2 = new Track("Epictrack", new SectionTypes[]
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
            Track[] tracks = new Track[] { track1, track2 };
            Controller.Data.AddTracks(tracks, _competition);
            Assert.AreEqual(tracks, _competition.Tracks);
        }
    }
}
