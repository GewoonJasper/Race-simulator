using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console_project;
using Controller;
using Model;
using NUnit.Framework;

namespace ControllerTest
{
    [TestFixture]
    class ConsoleProject_Visualisation_CheckForDriversShould
    {
        private Driver driver1;
        private Driver driver2;
        private Driver driver3;

        private Driver[] drivers;

        private Track track1;

        [SetUp]
        public void SetUp()
        {
            driver1 = new Driver("Verstappen", 0, IParticipant.TeamColors.Blue);
            driver2 = new Driver("Hamilton", 0, IParticipant.TeamColors.Grey);
            driver3 = new Driver("Norris", 0, IParticipant.TeamColors.Yellow);

            drivers = new Driver[] { driver1, driver2, driver3};

            track1 = new Track("Oval", new Section.SectionTypes[]
            {
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

            Track[] tracks = new Track[] { track1 };

            Competition comp = new Competition();

            Data.Initialize(comp, drivers, tracks);

            Data.NextRace(comp);
        }

        [Test]
        public void CheckForDrivers_NoDriversChanged_ReturnDriversOnStartGrid()
        {
            int y = 0;

            for (int i = track1.Sections.Count - 1; i >= 0 ; i--)
            {
                if (track1.Sections.ElementAt(i).SectionType.Equals(Section.SectionTypes.StartGrid) && y < drivers.Length)
                {
                    int x = 0;

                    while (x < 2)
                    {
                        if (y < drivers.Length)
                        {
                            Assert.AreEqual(drivers[y], Visualisation.CheckForDrivers(track1.Sections.ElementAt(i))[x]);
                            y++;
                        }
                        else
                        {
                            Assert.IsNull(Visualisation.CheckForDrivers(track1.Sections.ElementAt(i))[x]);
                        }
                        x++;
                    }
                }
                else
                {
                    Assert.IsNull(Visualisation.CheckForDrivers(track1.Sections.ElementAt(i))[0]);
                    Assert.IsNull(Visualisation.CheckForDrivers(track1.Sections.ElementAt(i))[1]);
                }
            }
        }

        [Test]
        public void CheckForDrivers_OneDriverNotOnStart_ReturnDriversOnStartAndOtherDriver()
        {
            SectionData sd = new SectionData();
            sd.Left = driver1;

            Data.CurrentRace.Positions.Add(track1.Sections.ElementAt(6), sd);

            int y = 0;

            for (int i = track1.Sections.Count - 1; i >= 0; i--)
            {
                if (i == 6)
                {
                    Assert.AreEqual(driver1, Visualisation.CheckForDrivers(track1.Sections.ElementAt(i))[0]);
                    Assert.IsNull(Visualisation.CheckForDrivers(track1.Sections.ElementAt(1))[1]);
                }
                else
                {
                    if (track1.Sections.ElementAt(i).SectionType.Equals(Section.SectionTypes.StartGrid) &&
                        y < drivers.Length)
                    {
                        int x = 0;

                        while (x < 2)
                        {
                            if (y < drivers.Length)
                            {
                                Assert.AreEqual(drivers[y],
                                    Visualisation.CheckForDrivers(track1.Sections.ElementAt(i))[x]);
                                y++;
                            }
                            else
                            {
                                Assert.IsNull(Visualisation.CheckForDrivers(track1.Sections.ElementAt(i))[x]);
                            }

                            x++;
                        }
                    }
                    else
                    {
                        Assert.IsNull(Visualisation.CheckForDrivers(track1.Sections.ElementAt(i))[0]);
                        Assert.IsNull(Visualisation.CheckForDrivers(track1.Sections.ElementAt(i))[1]);
                    }
                }
            }
        }
    }
}
