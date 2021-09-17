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
            foreach (SectionTypes section in sections) 
            {
                Section newSection = new Section
                {
                    SectionType = section
                };
                Sections.AddLast(newSection);
            }
        }
    }
}
