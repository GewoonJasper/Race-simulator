using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using Model;
using NUnit.Framework;

namespace ControllerTest
{

    [TestFixture]
    class Controller_Race_SetRaceParamsShould
    {
        private List<IParticipant> drivers;
        private Race r;

        private IParticipant driver1;
        private IParticipant driver2;
        private IParticipant driver3;
        private IParticipant driver4;

        private Track track1;
        [SetUp]
        public void SetUp()
        {
            driver1 = new Driver("Verstappen", 0, IParticipant.TeamColors.Blue);
            driver2 = new Driver("Hamilton", 0, IParticipant.TeamColors.Grey);
            driver3 = new Driver("Norris", 0, IParticipant.TeamColors.Yellow);
            driver4 = new Driver("Sainz", 0, IParticipant.TeamColors.Red);

            drivers = new List<IParticipant> { driver1, driver2, driver3, driver4 };

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

            r = new Race(track1, drivers);
        }

        [Test]
        public void SetRaceParams_Initalize_ReturnRightValues()
        {
           r.SetRaceParams(track1, 80, 8000);

           int x = 0;

           foreach (IParticipant participant in drivers)
           {
               Assert.AreEqual(r.Participants.ElementAt(x).Laps, 0);

               x++;
           }

           Assert.AreEqual(drivers.Count * 2, r.MaxPoints);

           int totalLaps = 8000 / (80 * track1.Sections.Count);
           Assert.AreEqual(totalLaps >= 2 ? totalLaps : 2, r.MaxLaps);
        }

        [Test]
        public void SetRaceParams_SectionLengthAndRaceLengthZero_ReturnZero()
        {
            r.SetRaceParams(track1, 0, 0);

            Assert.AreEqual(80, r.SectionLength);
            Assert.AreEqual(4000, r.RaceLength);
        }

        [Test]
        public void SetRaceParams_SectionLengthAndRaceLengthHaveValue_ReturnValue()
        {
            int value1 = 23;
            int value2 = 456;
            r.SetRaceParams(track1, value1, value2);

            Assert.AreEqual(r.SectionLength, value1);
            Assert.AreEqual(r.RaceLength, value2);
        }
    }
}
