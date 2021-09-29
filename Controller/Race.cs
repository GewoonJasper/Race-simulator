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

        public Race(Track track, List<IParticipant> participants)
        {
            this.Track = track;
            this.Participants = participants;
            this.StartTime = DateTime.Now;

            _positions = new Dictionary<Section, SectionData>();

            SetStartPositions(track, participants);
        }

        public SectionData GetSectionData(Section section)
        {
             if (!_positions.ContainsKey(section))
             {
                SectionData data = new SectionData();
                _positions.Add(section, data);
             }

            return _positions[section];
        }

        public void RandomizeEquipment()
        {
            foreach (Driver driver in Participants)
            {
                driver.Equipment.Quality = _random.Next();
                driver.Equipment.Performance = _random.Next();
            }
        }

        public void SetStartPositions(Track track, List<IParticipant> participants)
        {
            int Drivers = participants.Count();
            int Driver = 0;

            foreach (Section sectie in track.Sections) 
            {
                if (sectie.SectionType.Equals(Section.SectionTypes.StartGrid))
                {
                    SectionData GridData = GetSectionData(sectie);
                    int i = 0;

                    while (i < 2)
                    {
                        if (Drivers > Driver)
                        {
                        
                            if (GridData.Left == null)
                            {
                                GridData.Left = participants[Driver];
                                _positions[sectie] = GridData;
                            }
                            else if (GridData.Right == null)
                            {
                                GridData.Right = participants[Driver];
                                _positions[sectie] = GridData;
                            }
                            Driver++;
                        }
                        i++;
                    }

                }
            }

        }
    }
}
