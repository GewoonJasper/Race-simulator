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
    class Controller_Race_GetRaceLeaderboardShould
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
        public void GetRaceLeaderboard_AllZeroLaps_ReturnInsertOrder()
        {
            int x = 0;

            foreach (IParticipant participant in drivers)
            {
                Assert.AreEqual(participant, r.GetRaceLeaderboard()[x]);

                x++;
            }
        }

        [Test]
        public void GetRaceLeaderboard_DifferentLaps_ReturnMaxLapsLowLapsOrder()
        {
            r.Participants[1].Laps = 1;
            r.Participants[3].Laps = 1;

            Assert.AreEqual(drivers[0], r.GetRaceLeaderboard()[2]);
            Assert.AreEqual(drivers[1], r.GetRaceLeaderboard()[0]);
            Assert.AreEqual(drivers[2], r.GetRaceLeaderboard()[3]);
            Assert.AreEqual(drivers[3], r.GetRaceLeaderboard()[1]);


            r.Participants[0].Laps = 3;
            r.Participants[2].Laps = 1;
            r.Participants[3].Laps = 2;

            Assert.AreEqual(drivers[0], r.GetRaceLeaderboard()[0]);
            Assert.AreEqual(drivers[1], r.GetRaceLeaderboard()[2]);
            Assert.AreEqual(drivers[2], r.GetRaceLeaderboard()[3]);
            Assert.AreEqual(drivers[3], r.GetRaceLeaderboard()[1]);
        }
    }
}
