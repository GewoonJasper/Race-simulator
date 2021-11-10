using System;
using System.Collections.Generic;
using SectionTypes = Model.Section.SectionTypes;

namespace Model
{
    public class Track
    {
        public string Name { get; private set; }
        public LinkedList<Section> Sections { get; private set; }
        public int StartOrientation { get; private set; }

        public Track (string name, SectionTypes[] sections, int startOrientation)
        {
            Name = name;
            Sections = new LinkedList<Section>();
            Sections = ChangeTypesToSections(sections);

            StartOrientation = startOrientation >= 0 && startOrientation <=3 ? startOrientation : 1;
        }

        //Zet een array van sectionTypes om naar een linkedlist van sections
        private static LinkedList<Section> ChangeTypesToSections(SectionTypes[] sections)
        {
            LinkedList<Section> S = new();

            foreach (SectionTypes section in sections)
            {
                Section newSection = new()
                {
                    SectionType = section
                };

                S.AddLast(newSection);
            }

            return S;
        }
    }
}
