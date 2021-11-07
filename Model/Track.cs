using System;
using System.Collections.Generic;
using SectionTypes = Model.Section.SectionTypes;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }

        public Track (String name, SectionTypes[] sections)
        {
            Sections = new LinkedList<Section>();
            Name = name;
            Sections = ChangeTypesToSections(sections);
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
