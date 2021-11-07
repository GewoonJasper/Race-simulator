using NUnit.Framework;
using Model;

namespace ControllerTest
{
    [TestFixture]
    class Controller_Data_AddParticipantsShould
    {
        private Competition _competition;

        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
        }

        [Test]
        public void AddParticipants_EmptyArray_ReturnNull()
        {
            Driver[] drivers = new Driver[1];
            Controller.Data.AddParticipants(drivers, _competition);
            Assert.IsNull(_competition.Participants[0]);
        }

        [Test]
        public void AddParticipants_OneInArray_ReturnDriver()
        {
            Driver driver = new Driver("Verstappen", 0, IParticipant.TeamColors.Blue);
            Driver[] drivers = new Driver[] { driver };
            Controller.Data.AddParticipants(drivers, _competition);
            Assert.AreEqual(drivers, _competition.Participants);
        }

        [Test]
        public void AddParticipants_MoreInArray_ReturnDrivers()
        {
            Driver driver1 = new Driver("Verstappen", 0, IParticipant.TeamColors.Blue);
            Driver driver2 = new Driver("Hamilton", 0, IParticipant.TeamColors.Grey);
            Driver[] drivers = new Driver[] { driver1, driver2 };
            Controller.Data.AddParticipants(drivers, _competition);
            Assert.AreEqual(drivers, _competition.Participants);
        }
    }
}
