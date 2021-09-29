using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_project
{
    public static class Visualisation
    {
        public static void Initialize()
        {

        }

        #region graphics

        private static string[] _finishHorizontal = new string[] { "--------", "     #  ", "     #  ", "     #  ", "     #  ", "     #  ", "     #  ", "--------" };
        private static string[] _straightHorizontal = new string[] { "--------", "        ", "        ", "        ", "        ", "        ", "        ", "--------" };
        private static string[] _startHorizontal = new string[] { "--------", "        ", ". . A . ", "        ", "        ", " . B . .", "        ", "--------" };

        private static string[] _finishVertical = new string[] { "|      |", "|      |", "|      |", "|      |", "|      |", "|######|", "|      |", "|      |" };
        private static string[] _straightVertical = new string[] { "|      |", "|      |", "|      |", "|      |", "|      |", "|      |", "|      |", "|      |" };
        private static string[] _startVertical = new string[] { "| .    |", "|    . |", "| .    |", "|    B |", "| A    |", "|    . |", "| .    |", "|    . |" };

        private static string[] _cornerRightDown = new string[] { "----\\   ", "     \\  ", "      \\ ", "       \\", "       |", "       |", "       |", "\\      |" };
        private static string[] _cornerDownLeft = new string[] { "/      |", "       |", "       |", "       |", "       /", "      / ", "     /  ", "----/   " };
        private static string[] _cornerDownRight = new string[] { "|      \\", "|       ", "|       ", "|       ", "\\       ", " \\      ", "  \\     ", "   \\----" };
        private static string[] _cornerLeftDown = new string[] { "   /----", "  /     ", " /      ", "/       ", "|       ", "|       ", "|       ", "|      /" };

        #endregion

        public static void DrawTrack(Track track)
        {
            int cursorH = 1; // 0,0 is linksboven, dit is 1,... Het wordt per 8 tekens gerekend dus 1 * 8 = 8.
            int cursorV = 1; // 0,0 is linksboven, dit is ...,1 Het wordt per 8 tekens gerekend dus 1 * 8 = 8.
            int orientation = 1; // 0 = N, 1 = E, 2 = S, 3 = W

            foreach (Section section in track.Sections)
            {
                IParticipant[] driversInSection = CheckForDrivers(section);
                if (section.SectionType.Equals(Section.SectionTypes.RightCorner))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.SetCursorPosition(cursorH * 8, cursorV * 8 + i);
                        switch (orientation)
                        {
                            case 0:
                                Console.WriteLine(_cornerLeftDown[i]);
                            break;
                            case 1:
                                    Console.WriteLine(_cornerRightDown[i]);
                                break;
                            case 2:
                                    Console.WriteLine(_cornerDownLeft[i]);
                                break;
                            case 3:
                                    Console.WriteLine(_cornerDownRight[i]);
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
                                Console.WriteLine(_cornerRightDown[i]);
                                break;
                            case 1:
                                Console.WriteLine(_cornerDownLeft[i]);
                                break;
                            case 2:
                                Console.WriteLine(_cornerDownRight[i]);
                                break;
                            case 3:
                                Console.WriteLine(_cornerLeftDown[i]);
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
                                Console.WriteLine(_straightVertical[i]);
                                break;
                            case 1:
                                Console.WriteLine(_straightHorizontal[i]);
                                break;
                            case 2:
                                Console.WriteLine(_straightVertical[i]);
                                break;
                            case 3:
                                Console.WriteLine(_straightHorizontal[i]);
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
                                Console.WriteLine(_finishVertical[i]);
                                break;
                            case 1:
                                Console.WriteLine(_finishHorizontal[i]);
                                break;
                            case 2:
                                Console.WriteLine(_finishVertical[i]);
                                break;
                            case 3:
                                Console.WriteLine(_finishHorizontal[i]);
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
                                Console.WriteLine(_startVertical[i]);
                                break;
                            case 1:
                                string SectionLine = ChangeString(_startHorizontal[i], driversInSection[0], driversInSection[1]);
                                Console.WriteLine(SectionLine);
                                break;
                            case 2:
                                Console.WriteLine(_startVertical[i]);
                                break;
                            case 3:
                                Console.WriteLine(_startHorizontal[i]);
                                break;
                        }
                    }
                }

                if (orientation == 0)
                {
                    cursorV--;
                } else if (orientation == 1)
                {
                    cursorH++;
                } else if (orientation == 2)
                {
                    cursorV++;
                } else
                {
                    cursorH--;
                }
            }
        }

        public static IParticipant[] CheckForDrivers(Section section)
        {
            SectionData data = Data.CurrentRace.GetSectionData(section);
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

        public static string ChangeString(string tekst, IParticipant LeftDriver, IParticipant RightDriver)
        {
            if (LeftDriver == null)
            {
                tekst = tekst.Replace("A", ".");
            } else
            {
                tekst = tekst.Replace("A", LeftDriver.Name.Substring(0,1));
            }

            if (RightDriver == null)
            {
                tekst = tekst.Replace("B", ".");
            } else
            {
                tekst = tekst.Replace("B", RightDriver.Name.Substring(0,1));
            }

            return tekst;
        }
    }
}