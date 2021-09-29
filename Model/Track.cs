using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SectionTypes = Model.Section.SectionTypes;

namespace Model
{
    public class Track
    {
        public String name { get; set; }
        public LinkedList<Section> Sections { get; set; }

        public Track (String name, SectionTypes[] sections)
        {
            Sections = new LinkedList<Section>();
            this.name = name;
            Sections = changeTypesToSections(sections);
        }

        private LinkedList<Section> changeTypesToSections(SectionTypes[] sections)
        {
            LinkedList<Section> S = new LinkedList<Section>();

            foreach (SectionTypes section in sections)
            {
                Section newSection = new Section
                {
                    SectionType = section
                };

                S.AddLast(newSection);
            }

            return S;
        }
    }
}
