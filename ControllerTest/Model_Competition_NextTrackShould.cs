using Model;
using NUnit.Framework;
using static Model.Section;

namespace ControllerTest
{
    [TestFixture]
    public class Model_Competition_NextTrackShould
    {
        private Competition _competition { get; set; }

        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
        }

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            Track result = _competition.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            SectionTypes[] baan = new SectionTypes[7];
            baan[0] = SectionTypes.StartGrid;
            baan[1] = SectionTypes.Finish;
            baan[2] = SectionTypes.RightCorner;
            baan[3] = SectionTypes.RightCorner;
            baan[4] = SectionTypes.Straight;
            baan[5] = SectionTypes.RightCorner;
            baan[6] = SectionTypes.RightCorner;
            Track t = new("RightOval", baan, 1);

            _competition.Tracks.Enqueue(t);

            Track result = _competition.NextTrack();
            Assert.AreEqual(t, result);
        }

        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            SectionTypes[] baan = new SectionTypes[7];
            baan[0] = SectionTypes.StartGrid;
            baan[1] = SectionTypes.Finish;
            baan[2] = SectionTypes.RightCorner;
            baan[3] = SectionTypes.RightCorner;
            baan[4] = SectionTypes.Straight;
            baan[5] = SectionTypes.RightCorner;
            baan[6] = SectionTypes.RightCorner;
            Track t = new("RightOval", baan, 1);

            _competition.Tracks.Enqueue(t);

            _competition.NextTrack();
            Track result = _competition.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            SectionTypes[] baan = new SectionTypes[7];
            baan[0] = SectionTypes.StartGrid;
            baan[1] = SectionTypes.Finish;
            baan[2] = SectionTypes.RightCorner;
            baan[3] = SectionTypes.RightCorner;
            baan[4] = SectionTypes.Straight;
            baan[5] = SectionTypes.RightCorner;
            baan[6] = SectionTypes.RightCorner;
            Track t = new("RightOval", baan, 1);

            SectionTypes[] baan2 = new SectionTypes[7];
            baan2[0] = SectionTypes.StartGrid;
            baan2[1] = SectionTypes.Finish;
            baan2[2] = SectionTypes.LeftCorner;
            baan2[3] = SectionTypes.LeftCorner;
            baan2[4] = SectionTypes.Straight;
            baan2[5] = SectionTypes.LeftCorner;
            baan2[6] = SectionTypes.LeftCorner;
            Track t2 = new("LeftOval", baan2, 1);

            _competition.Tracks.Enqueue(t);
            _competition.Tracks.Enqueue(t2);

            Track result = _competition.NextTrack();
            Assert.AreEqual(t, result);

            result = _competition.NextTrack();
            Assert.AreEqual(t2, result);

            result = _competition.NextTrack();
            Assert.IsNull(result);
        }
    }
}
