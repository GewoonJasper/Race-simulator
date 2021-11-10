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
    class Controller_Data_GetCompetitionLeaderboardShould
    {
        private Competition competition;

        private Driver[] drivers;
        private Track[] tracks;

        private Driver driver1;
        private Driver driver2;
        private Driver driver3;
        private Driver driver4;

        private Track track1;
        [SetUp]
        public void SetUp()
        {
            competition = new Competition();

            driver1 = new Driver("Verstappen", 0, IParticipant.TeamColors.Blue);
            driver2 = new Driver("Hamilton", 0, IParticipant.TeamColors.Grey);
            driver3 = new Driver("Norris", 0, IParticipant.TeamColors.Yellow);
            driver4 = new Driver("Sainz", 0, IParticipant.TeamColors.Red);

            drivers = new Driver[] { driver1, driver2, driver3, driver4 };

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

            tracks = new Track[] { track1 };

            Data.Initialize(competition, drivers, tracks);
        }

        [Test]
        public void GetCompetitionLeaderboard_AllZeroPoints_ReturnInsertOrder()
        {
            int x = 0;

            foreach (Driver driver in drivers)
            {
                Assert.AreEqual(driver, Data.GetCompetitionLeaderboard(competition)[x]);

                x++;
            }
        }

        [Test]
        public void GetCompetitionLeaderboard_DifferentPoints_ReturnMaxPointsLowPointsOrder()
        {
            Data.Competition.Participants[0].Points = 10;
            Data.Competition.Participants[3].Points = 15;
            Data.Competition.Participants[1].Points = 8;

            Assert.AreEqual(drivers[0], Data.GetCompetitionLeaderboard(competition)[1]);
            Assert.AreEqual(drivers[1], Data.GetCompetitionLeaderboard(competition)[2]);
            Assert.AreEqual(drivers[2], Data.GetCompetitionLeaderboard(competition)[3]);
            Assert.AreEqual(drivers[3], Data.GetCompetitionLeaderboard(competition)[0]);


            Data.Competition.Participants[2].Points = 16;
            Data.Competition.Participants[3].Points = 1;

            Assert.AreEqual(drivers[0], Data.GetCompetitionLeaderboard(competition)[1]);
            Assert.AreEqual(drivers[1], Data.GetCompetitionLeaderboard(competition)[2]);
            Assert.AreEqual(drivers[2], Data.GetCompetitionLeaderboard(competition)[0]);
            Assert.AreEqual(drivers[3], Data.GetCompetitionLeaderboard(competition)[3]);
        }
    }
}
