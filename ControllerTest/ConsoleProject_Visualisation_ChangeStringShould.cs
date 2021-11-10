using Console_project;
using Model;
using NUnit.Framework;

namespace ControllerTest
{
    [TestFixture]
    class ConsoleProject_Visualisation_ChangeStringShould
    {
        private string s1;
        private string s2;
        private string s3;

        private IParticipant driver1;
        private IParticipant driver2;

        [SetUp]
        public void SetUp()
        {
            s1 = "     1] ";
            s2 = " 2]     ";
            s3 = "        ";

            driver1 = new Driver("Verstappen", 0, IParticipant.TeamColors.Blue);
            driver2 = new Driver("Hamilton", 0, IParticipant.TeamColors.Grey);
        }

        [Test]
        public void ChangeString_NoDriversInStection_ReturnString()
        {
            Assert.AreEqual("      ] ", Visualisation.ChangeString(s1, null, null));
            Assert.AreEqual("  ]     ", Visualisation.ChangeString(s2, null, null));
            Assert.AreEqual("        ", Visualisation.ChangeString(s3, null, null));
        }

        [Test]
        public void ChangeString_LeftDriver1NotBrokenInStection_ReturnString()
        {
            Assert.AreEqual("     V] ", Visualisation.ChangeString(s1, driver1, null));
            Assert.AreEqual("  ]     ", Visualisation.ChangeString(s2, driver1, null));
            Assert.AreEqual("        ", Visualisation.ChangeString(s3, driver1, null));
        }

        [Test]
        public void ChangeString_LeftDriver1BrokenInStection_ReturnString()
        {
            driver1.Car.IsBroken = true;
            driver2.Car.IsBroken = true;

            Assert.AreEqual("     v] ", Visualisation.ChangeString(s1, driver1, null));
            Assert.AreEqual("  ]     ", Visualisation.ChangeString(s2, driver1, null));
            Assert.AreEqual("        ", Visualisation.ChangeString(s3, driver1, null));
        }

        [Test]
        public void ChangeString_LeftDriver2NotBrokenInStection_ReturnString()
        {
            Assert.AreEqual("     H] ", Visualisation.ChangeString(s1, driver2, null));
            Assert.AreEqual("  ]     ", Visualisation.ChangeString(s2, driver2, null));
            Assert.AreEqual("        ", Visualisation.ChangeString(s3, driver2, null));
        }

        [Test]
        public void ChangeString_LeftDriver2BrokenInStection_ReturnString()
        {
            driver1.Car.IsBroken = true;
            driver2.Car.IsBroken = true;

            Assert.AreEqual("     h] ", Visualisation.ChangeString(s1, driver2, null));
            Assert.AreEqual("  ]     ", Visualisation.ChangeString(s2, driver2, null));
            Assert.AreEqual("        ", Visualisation.ChangeString(s3, driver2, null));
        }

        [Test]
        public void ChangeString_RightDriver1NotBrokenInStection_ReturnString()
        {
            Assert.AreEqual("      ] ", Visualisation.ChangeString(s1, null, driver1));
            Assert.AreEqual(" V]     ", Visualisation.ChangeString(s2, null, driver1));
            Assert.AreEqual("        ", Visualisation.ChangeString(s3, null, driver1));
        }

        [Test]
        public void ChangeString_RightDriver1BrokenInStection_ReturnString()
        {
            driver1.Car.IsBroken = true;
            driver2.Car.IsBroken = true;

            Assert.AreEqual("      ] ", Visualisation.ChangeString(s1, null, driver1));
            Assert.AreEqual(" v]     ", Visualisation.ChangeString(s2, null, driver1));
            Assert.AreEqual("        ", Visualisation.ChangeString(s3, null, driver1));
        }

        [Test]
        public void ChangeString_RightDriver2NotBrokenInStection_ReturnString()
        {
            Assert.AreEqual("      ] ", Visualisation.ChangeString(s1, null, driver2));
            Assert.AreEqual(" H]     ", Visualisation.ChangeString(s2, null, driver2));
            Assert.AreEqual("        ", Visualisation.ChangeString(s3, null, driver2));
        }

        [Test]
        public void ChangeString_RightDriver2BrokenInStection_ReturnString()
        {
            driver1.Car.IsBroken = true;
            driver2.Car.IsBroken = true;

            Assert.AreEqual("      ] ", Visualisation.ChangeString(s1, null, driver2));
            Assert.AreEqual(" h]     ", Visualisation.ChangeString(s2, null, driver2));
            Assert.AreEqual("        ", Visualisation.ChangeString(s3, null, driver2));
        }

        [Test]
        public void ChangeString_BothDriversNotBrokenInStection1_ReturnString()
        {
            Assert.AreEqual("     V] ", Visualisation.ChangeString(s1, driver1, driver2));
            Assert.AreEqual(" H]     ", Visualisation.ChangeString(s2, driver1, driver2));
            Assert.AreEqual("        ", Visualisation.ChangeString(s3, driver1, driver2));
        }

        [Test]
        public void ChangeString_BothDriversBrokenInStection1_ReturnString()
        {
            driver1.Car.IsBroken = true;
            driver2.Car.IsBroken = true;

            Assert.AreEqual("     v] ", Visualisation.ChangeString(s1, driver1, driver2));
            Assert.AreEqual(" h]     ", Visualisation.ChangeString(s2, driver1, driver2));
            Assert.AreEqual("        ", Visualisation.ChangeString(s3, driver1, driver2));
        }

        [Test]
        public void ChangeString_BothDriversNotBrokenInStection2_ReturnString()
        {
            Assert.AreEqual("     H] ", Visualisation.ChangeString(s1, driver2, driver1));
            Assert.AreEqual(" V]     ", Visualisation.ChangeString(s2, driver2, driver1));
            Assert.AreEqual("        ", Visualisation.ChangeString(s3, driver2, driver1));
        }

        [Test]
        public void ChangeString_BothDriversBrokenInStection2_ReturnString()
        {
            driver1.Car.IsBroken = true;
            driver2.Car.IsBroken = true;

            Assert.AreEqual("     h] ", Visualisation.ChangeString(s1, driver2, driver1));
            Assert.AreEqual(" v]     ", Visualisation.ChangeString(s2, driver2, driver1));
            Assert.AreEqual("        ", Visualisation.ChangeString(s3, driver2, driver1));
        }
    }
}
