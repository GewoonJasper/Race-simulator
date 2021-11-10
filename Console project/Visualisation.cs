using Controller;
using Model;
using System;

namespace Console_project
{
    public static class Visualisation
    {
        public static int CursorH { get; private set; } // 0,0 is linksboven
        public static int CursorV { get; private set; } // 0,0 is linksboven
        public static int Orientation { get; private set; } // 0 = N, 1 = E, 2 = S, 3 = W

        // Alle mogelijkheden getekend in stringarrays
        #region graphics

        private static readonly string[] _finishHorizontal = new string[] 
        { 
            "--------",
            "     #  ",
            "   1 #  ",
            "     #  ",
            "     #  ",
            "   2 #  ",
            "     #  ",
            "--------"
        };

        private static readonly string[] _straightHorizontal = new string[]
        {
            "--------",
            "        ",
            "    1   ",
            "        ",
            "        ",
            "    2   ",
            "        ",
            "--------"
        };

        private static readonly string[] _startHorizontal = new string[]
        {
            "--------",
            "        ",
            "     1] ",
            "        ",
            "        ",
            " 2]     ",
            "        ",
            "--------"
        };

        
        private static readonly string[] _finishVertical = new string[]
        {
            "|      |",
            "|      |",
            "|    2 |",
            "|      |",
            "| 1    |",
            "|######|",
            "|      |",
            "|      |"
        };
        

        private static readonly string[] _straightVertical = new string[]
        {
            "|      |",
            "|      |",
            "|      |",
            "| 2  1 |",
            "|      |",
            "|      |",
            "|      |",
            "|      |"
        };

        
        private static readonly string[] _startVertical = new string[]
        {
            "|      |",
            "|      |",
            "|    2 |",
            "|    - |",
            "|      |",
            "|      |",
            "| 1    |",
            "| -    |"
        };
        

        private static readonly string[] _cornerRightDown = new string[]
        {
            @"----\   ",
            @"     \  ",
            @"    1 \ ",
            @"       \",
            @"       |",
            @"  2    |",
            @"       |",
            @"\      |"
        };

        private static readonly string[] _cornerDownLeft = new string[]
        {
            "/      |",
            "       |",
            "  2    |",
            "       |",
            "       /",
            "    1 / ",
            "     /  ",
            "----/   "
        };

        private static readonly string[] _cornerDownRight = new string[]
        {
            @"|      \",
            @"|       ",
            @"|    2  ",
            @"|       ",
            @"\       ",
            @" \ 1    ",
            @"  \     ",
            @"   \----"
        };

        private static readonly string[] _cornerLeftDown = new string[]
        {
            "   /----",
            "  /     ",
            " / 1    ",
            "/       ",
            "|       ",
            "|    2  ",
            "|       ",
            "|      /"
        };

        #endregion

        // Tekent de baan
        public static void DrawTrack(Track track)
        {
            CheckTrackPosition(track);

            Console.CursorVisible = false; // Zorgt ervoor dat je de cursor niet de hele tijd heen en weer ziet springen
            string SectionLine;

            foreach (Section section in track.Sections)
            {
                // Geeft drivers in een sectie terug, array van maximaal 2 lang omdat er maar 2 drivers in een sectie kunnen zitten
                IParticipant[] driversInSection = CheckForDrivers(section);
                
                // Per sectietype wordt er iets anders geprint
                // Elke type moet 8 regels printen van boven naar beneden
                if (section.SectionType.Equals(Section.SectionTypes.RightCorner))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.SetCursorPosition(CursorH * 8, CursorV * 8 + i);
                        switch (Orientation)
                        {
                            case 0:
                                SectionLine = ChangeString(_cornerLeftDown[i], driversInSection[0], driversInSection[1]);
                                Console.WriteLine(SectionLine);
                            break;
                            case 1:
                                SectionLine = ChangeString(_cornerRightDown[i], driversInSection[0], driversInSection[1]);
                                Console.WriteLine(SectionLine);
                                break;
                            case 2:
                                SectionLine = ChangeString(_cornerDownLeft[i], driversInSection[0], driversInSection[1]);
                                Console.WriteLine(SectionLine);
                                break;
                            case 3:
                                SectionLine = ChangeString(_cornerDownRight[i], driversInSection[0], driversInSection[1]);
                                Console.WriteLine(SectionLine);
                                break;
                        }
                    }

                    Orientation++;
                    if (Orientation > 3)
                    {
                        Orientation = 0;
                    }

                }
                else if (section.SectionType.Equals(Section.SectionTypes.LeftCorner))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.SetCursorPosition(CursorH * 8, CursorV * 8 + i);
                        switch (Orientation)
                        {
                            case 0:
                                SectionLine = ChangeString(_cornerRightDown[i], driversInSection[0], driversInSection[1]);
                                Console.WriteLine(SectionLine);
                                break;
                            case 1:
                                SectionLine = ChangeString(_cornerDownLeft[i], driversInSection[0], driversInSection[1]);
                                Console.WriteLine(SectionLine);
                                break;
                            case 2:
                                SectionLine = ChangeString(_cornerDownRight[i], driversInSection[0], driversInSection[1]);
                                Console.WriteLine(SectionLine);
                                break;
                            case 3:
                                SectionLine = ChangeString(_cornerLeftDown[i], driversInSection[0], driversInSection[1]);
                                Console.WriteLine(SectionLine);
                                break;
                        }
                    }

                    Orientation--;
                    if (Orientation < 0)
                    {
                        Orientation = 3;
                    }

                }
                else if (section.SectionType.Equals(Section.SectionTypes.Straight))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.SetCursorPosition(CursorH * 8, CursorV * 8 + i);
                        switch (Orientation)
                        {
                            case 0:
                                SectionLine = ChangeString(_straightVertical[i], driversInSection[0], driversInSection[1]);
                                Console.WriteLine(SectionLine);
                                break;
                            case 1:
                                SectionLine = ChangeString(_straightHorizontal[i], driversInSection[0], driversInSection[1]);
                                Console.WriteLine(SectionLine);
                                break;
                            case 2:
                                SectionLine = ChangeString(_straightVertical[i], driversInSection[0], driversInSection[1]);
                                Console.WriteLine(SectionLine);
                                break;
                            case 3:
                                SectionLine = ChangeString(_straightHorizontal[i], driversInSection[0], driversInSection[1]);
                                Console.WriteLine(SectionLine);
                                break;
                        }
                    }
                }
                else if (section.SectionType.Equals(Section.SectionTypes.Finish))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.SetCursorPosition(CursorH * 8, CursorV * 8 + i);
                        switch (Orientation)
                        {
                            case 0:
                                SectionLine = ChangeString(_finishVertical[i], driversInSection[0], driversInSection[1]);
                                Console.WriteLine(SectionLine);
                                break;
                            case 1:
                                SectionLine = ChangeString(_finishHorizontal[i], driversInSection[0], driversInSection[1]);
                                Console.WriteLine(SectionLine);
                                break;
                            case 2:
                                SectionLine = ChangeString(_finishVertical[i], driversInSection[0], driversInSection[1]);
                                Console.WriteLine(SectionLine);
                                break;
                            case 3:
                                SectionLine = ChangeString(_finishHorizontal[i], driversInSection[0], driversInSection[1]);
                                Console.WriteLine(SectionLine);
                                break;
                        }
                    }
                }
                else if (section.SectionType.Equals(Section.SectionTypes.StartGrid))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.SetCursorPosition(CursorH * 8, CursorV * 8 + i);
                        switch (Orientation)
                        {
                            case 0:
                                SectionLine = ChangeString(_startVertical[i], driversInSection[0], driversInSection[1]);
                                Console.WriteLine(SectionLine);
                                break;
                            case 1:
                                SectionLine = ChangeString(_startHorizontal[i], driversInSection[0], driversInSection[1]);
                                Console.WriteLine(SectionLine);
                                break;
                            case 2:
                                SectionLine = ChangeString(_startVertical[i], driversInSection[0], driversInSection[1]);
                                Console.WriteLine(SectionLine);
                                break;
                            case 3:
                                SectionLine = ChangeString(_startHorizontal[i], driversInSection[0], driversInSection[1]);
                                Console.WriteLine(SectionLine);
                                break;
                        }
                    }
                }

                //Veranderd CursorH of CursorV, gekeken naaar de kant waar hij naartoe staat
                if (Orientation == 0)
                {
                    CursorV--;
                } 
                else if (Orientation == 1)
                {
                    CursorH++;
                } 
                else if (Orientation == 2)
                {
                    CursorV++;
                } 
                else
                {
                    CursorH--;
                }
            }
        }

        // Checkt hoe ver de baan van de kant af moet zijn
        public static void CheckTrackPosition(Track track)
        {
            Orientation = track.StartOrientation;

            int laagsteX = 0;
            int laagsteY = 0;

            CursorH = 0;
            CursorV = 0;

            // Het tekenen van de baan op de juiste plekken
            foreach (Section section in track.Sections)
            {
                if (section.SectionType.Equals(Section.SectionTypes.RightCorner))
                {
                    Orientation++;
                    if (Orientation > 3)
                    {
                        Orientation = 0;
                    }

                }
                else if (section.SectionType.Equals(Section.SectionTypes.LeftCorner))
                {
                    Orientation--;
                    if (Orientation < 0)
                    {
                        Orientation = 3;
                    }

                }

                if (Orientation == 0)
                {
                    CursorV--;
                }
                else if (Orientation == 1)
                {
                    CursorH++;
                }
                else if (Orientation == 2)
                {
                    CursorV++;
                }
                else
                {
                    CursorH--;
                }

                if (CursorH < laagsteX)
                {
                    laagsteX = CursorH;
                }

                if (CursorV < laagsteY)
                {
                    laagsteY = CursorV;
                }
            }

            if (laagsteX < 0)
            {
                laagsteX *= -1;
            }
            if (laagsteY < 0)
            {
                laagsteY *= -1;
            }

            CursorH = laagsteX + 1;
            CursorV = laagsteY + 1;
        }

        // Checkt voor de drivers in een sectie, kan max 2 drivers returnen
        public static IParticipant[] CheckForDrivers(Section section)
        {
            SectionData data = Race.GetSectionData(section, Data.CurrentRace.Positions);
            IParticipant[] participants = new IParticipant[2];

            if (data.Left != null)
            {
                participants[0] = data.Left;
            }

            if (data.Right != null)
            {
                participants[1] = data.Right;
            }

            return participants;
        }

        // Veranderd de string welke getekend wordt op de baan
        // Als de driver die in CheckForDrivers meegegeven is null is, dan wordt het cijfer vervangen door een spatie
        // Anders wordt het vervangen door de eerste letter van de driver
        public static string ChangeString(string tekst, IParticipant LeftDriver, IParticipant RightDriver)
        {
            if (LeftDriver == null)
            {
                tekst = tekst.Replace("1", " ");
            } else if (LeftDriver.Car.IsBroken)
            {
                tekst = tekst.Replace("1", LeftDriver.Name.Substring(0, 1).ToLower());
            }
            else
            {
                tekst = tekst.Replace("1", LeftDriver.Name.Substring(0,1));
            }

            if (RightDriver == null)
            {
                tekst = tekst.Replace("2", " ");
            }
            else if (RightDriver.Car.IsBroken)
            {
                tekst = tekst.Replace("2", RightDriver.Name.Substring(0, 1).ToLower());
            }
            else
            {
                tekst = tekst.Replace("2",  RightDriver.Name.Substring(0,1));
            }

            return tekst;
        }

        // Dit is de eventhandler die aangeroepen wordt wanneer de driverschanged event in actie komt, dus elke keer wanneer de timer af gaat
        // Het tekent de baan
        public static void OnDriversChanged(Object o, DriversChangedEventArgs e)
        {
            DrawTrack(e.Track);
        }

        // Dit is de eventhandler die aangeroepen wordt wanneer de racechanged event in actie komt, dus wanneer er in data een nieuwe race aangemaakt wordt
        public static void OnRaceChanged(Object o, EventArgs e)
        {
            ClearTrack();
            Race r = (Race)o;
            r.DriversChanged += OnDriversChanged;
        }

        // Maakt de console leeg
        private static void ClearTrack()
        {
            Console.Clear();
        }
    }
}