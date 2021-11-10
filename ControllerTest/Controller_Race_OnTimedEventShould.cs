using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Controller;
using Model;
using NUnit.Framework;

namespace ControllerTest
{
    [TestFixture]
    class Controller_Race_OnTimedEventShould
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
        public void OnTimedEvent_text_text()
        {
            //r.OnTimedEvent(r, new ElapsedEventArgs());
        }
    }
}
