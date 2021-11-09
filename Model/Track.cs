using System;
using System.Collections.Generic;
using SectionTypes = Model.Section.SectionTypes;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }
        public int StartOrientation { get; set; }

        public Track (String name, SectionTypes[] sections, int startOrientation)
        {
            Name = name;
            Sections = new LinkedList<Section>();
            Sections = ChangeTypesToSections(sections);

            StartOrientation = startOrientation >= 0 && startOrientation <=3 ? startOrientation : 1;
        }

        //Zet een array van sectionTypes om naar een linkedlist van sections
        private LinkedList<Section> ChangeTypesToSections(SectionTypes[] sections)
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
