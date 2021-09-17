using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }

        private Random _random = new Random(DateTime.Now.Millisecond);

        private Dictionary<Section, SectionData> _positions;

        public SectionData GetSectionData(Section section)
        {
             if (_positions[section] == null)
             {
                SectionData data = new SectionData();
                _positions.Add(section, data);
             }

            return _positions[section];
        }

        public Race(Track track, List<IParticipant> participants)
        {
            this.Track = track;
            this.Participants = participants;
            this.StartTime = DateTime.Now;
        }

        public void RandomizeEquipment()
        {
            foreach (Driver driver in Participants)
            {
                driver.Equipment.Quality = _random.Next();
                driver.Equipment.Performance = _random.Next();
            }
        }
    }
}
