using Console_project;
using Model;
using NUnit.Framework;

namespace ControllerTest
{
    [TestFixture]
    class ConsoleProject_Visualisation_CheckTrackPositionShould
    {
        private Track track1;
        private Track track2;
        private Track track3;
        private Track track4;
        private Track track5;

        [SetUp]
        public void SetUp()
        {
            track1 = new Track("Oval", new Section.SectionTypes[]
            {
                Section.SectionTypes.StartGrid,
                Section.SectionTypes.StartGrid,
                Section.SectionTypes.StartGrid,
                Section.SectionTypes.StartGrid,
                Section.SectionTypes.Finish,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.RightCorner
            }, 1);
            track2 = new Track("Epictrack", new Section.SectionTypes[]
            {
                Section.SectionTypes.StartGrid,
                Section.SectionTypes.StartGrid,
                Section.SectionTypes.StartGrid,
                Section.SectionTypes.StartGrid,
                Section.SectionTypes.Finish,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.RightCorner
            }, 1);
            track3 = new Track("Bridges", new Section.SectionTypes[]
            {
                Section.SectionTypes.StartGrid,
                Section.SectionTypes.StartGrid,
                Section.SectionTypes.StartGrid,
                Section.SectionTypes.Finish,
                Section.SectionTypes.Straight,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.RightCorner
            }, 1);
            track4 = new Track("Veldbaan", new Section.SectionTypes[]
            {
                Section.SectionTypes.StartGrid,
                Section.SectionTypes.StartGrid,
                Section.SectionTypes.StartGrid,
                Section.SectionTypes.Finish,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner
            }, 1);
            track5 = new Track("VerticalStart", new Section.SectionTypes[]
            {
                Section.SectionTypes.Finish,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.StartGrid,
                Section.SectionTypes.StartGrid,
                Section.SectionTypes.StartGrid
            }, 2);
        }

        [Test]
        public void CheckTrackPosition_track1_ReturnRightValues()
        {
            Visualisation.CheckTrackPosition(track1);
            
            Assert.AreEqual(2, Visualisation.CursorH);
            Assert.AreEqual(1, Visualisation.CursorV);
        }

        [Test]
        public void CheckTrackPosition_track2_ReturnRightValues()
        {
            Visualisation.CheckTrackPosition(track2);

            Assert.AreEqual(2, Visualisation.CursorH);
            Assert.AreEqual(2, Visualisation.CursorV);
        }

        [Test]
        public void CheckTrackPosition_track3_ReturnRightValues()
        {
            Visualisation.CheckTrackPosition(track3);

            Assert.AreEqual(2, Visualisation.CursorH);
            Assert.AreEqual(2, Visualisation.CursorV);
        }

        [Test]
        public void CheckTrackPosition_track4_ReturnRightValues()
        {
            Visualisation.CheckTrackPosition(track4);

            Assert.AreEqual(2, Visualisation.CursorH);
            Assert.AreEqual(2, Visualisation.CursorV);
        }

        [Test]
        public void CheckTrackPosition_track5_ReturnRightValues()
        {
            Visualisation.CheckTrackPosition(track5);

            Assert.AreEqual(3, Visualisation.CursorH);
            Assert.AreEqual(5, Visualisation.CursorV);
        }
    }
}
