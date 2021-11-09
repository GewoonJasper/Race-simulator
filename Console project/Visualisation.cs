using Controller;
using Model;
using System;

namespace Console_project
{
    public static class Visualisation
    {
        public static int cursorH; // 0,0 is linksboven
        public static int cursorV; // 0,0 is linksboven
        public static int orientation; // 0 = N, 1 = E, 2 = S, 3 = W

        // Alle mogelijkheden getekend in stringarrays
        #region graphics

        private static string[] _finishHorizontal = new string[] 
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

        private static string[] _straightHorizontal = new string[]
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

        private static string[] _startHorizontal = new string[]
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

        
        private static string[] _finishVertical = new string[]
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
        

        private static string[] _straightVertical = new string[]
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

        
        private static string[] _startVertical = new string[]
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
        

        private static string[] _cornerRightDown = new string[]
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

        private static string[] _cornerDownLeft = new string[]
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

        private static string[] _cornerDownRight = new string[]
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

        private static string[] _cornerLeftDown = new string[]
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
            orientation = track.StartOrientation;

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
                        Console.SetCursorPosition(cursorH * 8, cursorV * 8 + i);
                        switch (orientation)
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

                    orientation++;
                    if (orientation > 3)
                    {
                        orientation = 0;
                    }

                }
                else if (section.SectionType.Equals(Section.SectionTypes.LeftCorner))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.SetCursorPosition(cursorH * 8, cursorV * 8 + i);
                        switch (orientation)
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

                    orientation--;
                    if (orientation < 0)
                    {
                        orientation = 3;
                    }

                }
                else if (section.SectionType.Equals(Section.SectionTypes.Straight))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.SetCursorPosition(cursorH * 8, cursorV * 8 + i);
                        switch (orientation)
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
                        Console.SetCursorPosition(cursorH * 8, cursorV * 8 + i);
                        switch (orientation)
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
                        Console.SetCursorPosition(cursorH * 8, cursorV * 8 + i);
                        switch (orientation)
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

                //Veranderd cursorH of cursorV, gekeken naaar de kant waar hij naartoe staat
                if (orientation == 0)
                {
                    cursorV--;
                } 
                else if (orientation == 1)
                {
                    cursorH++;
                } 
                else if (orientation == 2)
                {
                    cursorV++;
                } 
                else
                {
                    cursorH--;
                }
            }
        }

        // Checkt hoe ver de baan van de kant af moet zijn
        public static void CheckTrackPosition(Track track)
        {
            int laagsteX = 0;
            int laagsteY = 0;

            cursorH = 0;
            cursorV = 0;

            // Het tekenen van de baan op de juiste plekken
            foreach (Section section in track.Sections)
            {
                if (section.SectionType.Equals(Section.SectionTypes.RightCorner))
                {
                    orientation++;
                    if (orientation > 3)
                    {
                        orientation = 0;
                    }

                }
                else if (section.SectionType.Equals(Section.SectionTypes.LeftCorner))
                {
                    orientation--;
                    if (orientation < 0)
                    {
                        orientation = 3;
                    }

                }

                if (orientation == 0)
                {
                    cursorV--;
                }
                else if (orientation == 1)
                {
                    cursorH++;
                }
                else if (orientation == 2)
                {
                    cursorV++;
                }
                else
                {
                    cursorH--;
                }

                if (cursorH < laagsteX)
                {
                    laagsteX = cursorH;
                }

                if (cursorV < laagsteY)
                {
                    laagsteY = cursorV;
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

            cursorH = laagsteX + 1;
            cursorV = laagsteY + 1;
        }

        // Checkt voor de drivers in een sectie, kan max 2 drivers returnen
        public static IParticipant[] CheckForDrivers(Section section)
        {
            SectionData data = Data.CurrentRace.GetSectionData(section, Data.CurrentRace.Positions);
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

        // Dit is de eventhandler die aangeroepen wordt wanneer de driverschanged event in actie komt
        // Het tekent de baan
        public static void OnDriversChanged(Object o, DriversChangedEventArgs e)
        {
            DrawTrack(e.Track);
        }

        public static void OnRaceChanged(Object o, EventArgs e)
        {
            ClearTrack();
            Race r = (Race)o;
            r.DriversChanged += OnDriversChanged;
        }

        private static void ClearTrack()
        {
            Console.Clear();
        }
    }
}