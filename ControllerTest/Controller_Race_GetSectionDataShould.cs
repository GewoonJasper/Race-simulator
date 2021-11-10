using System.Collections.Generic;
using System.Linq;
using Controller;
using Model;
using NUnit.Framework;
using static Model.Section;

namespace ControllerTest
{
    [TestFixture]
    class Controller_Race_GetSectionDataShould
    {
        private Race _race;
        private Track _track1;

        private Dictionary<Section, SectionData> _positions;

        [SetUp]
        public void SetUp()
        {
            _track1 = new Track("Oval", new SectionTypes[]
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
            }, 1);
            _race = new Race(_track1, new List<IParticipant>());

            _positions = new Dictionary<Section, SectionData>();
        }

        [Test]
        public void GetSectionData_NoDataInDictionary_ReturnNull()
        {
            Assert.IsNull(Race.GetSectionData(null, _positions));
        }

        [Test]
        public void GetSectionData_NoDataInDictionary_AddData()
        {
            SectionData testData = Race.GetSectionData(_track1.Sections.ElementAt(0), _positions);
            Assert.AreEqual(_positions[_track1.Sections.ElementAt(0)], testData);
        }

        [Test]
        public void GetSectionData_DataInDictionary_ReturnData()
        {
            _positions[_track1.Sections.ElementAt(0)] = new SectionData();
            SectionData testData = Race.GetSectionData(_track1.Sections.ElementAt(0), _positions);
            Assert.AreEqual(_positions[_track1.Sections.ElementAt(0)], testData);
        }

        [Test]
        public void GetSectionData_NoDataInDictionary_AddFullTrack()
        {
            foreach (Section s in _race.Track.Sections)
            {
                SectionData testData = Race.GetSectionData(s, _positions);
                Assert.AreEqual(_positions[s], testData);
            }
        }

        [Test]
        public void GetSectionData_FullTrackInDictionary_ReturnFullTrack()
        {
            foreach (Section s in _race.Track.Sections)
            {
                _positions[s] = new SectionData();
            }

            foreach (Section s in _race.Track.Sections)
            {
                SectionData testData = Race.GetSectionData(s, _positions);
                Assert.AreEqual(_positions[s], testData);
            }
        }
    }
}
