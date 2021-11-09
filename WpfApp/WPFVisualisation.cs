using System.Collections.Generic;
using Controller;
using Model;
using System.Drawing;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Media.Imaging;
using System.Linq;

namespace WpfApp
{
    public static class WPFVisualisation
    {
        public static int cursorH; // 0,0 is linksboven
        public static int cursorV; // 0,0 is linksboven
        public static int orientation; // 0 = N, 1 = E, 2 = S, 3 = W

        #region Images

        private const string _finishNorth = @".\Art\finishNorth.png";
        private const string _finishEast = @".\Art\finishEast.png";
        private const string _finishSouth = @".\Art\finishSouth.png";
        private const string _finishWest = @".\Art\finishWest.png";

        private const string _startHorizontalNorth = @".\Art\startGridNorth.png";
        private const string _startHorizontalEast = @".\Art\startGridEast.png";
        private const string _startHorizontalSouth = @".\Art\startGridSouth.png";
        private const string _startHorizontalWest = @".\Art\startGridWest.png";

        private const string _straightHorizontal = @".\Art\straightHorizontal.png";
        private const string _straightVertical = @".\Art\straightVertical.png";

        private const string _cornerDownLeft = @".\Art\cornerDownLeft.png";
        private const string _cornerDownRight = @".\Art\cornerDownRight.png";
        private const string _cornerLeftDown = @".\Art\cornerLeftDown.png";
        private const string _cornerRightDown = @".\Art\cornerRightDown.png";

        private const string _redCar = @".\Art\redCar.png";
        private const string _redCarBroken = @".\Art\redCarBroken.png";

        private const string _greenCar = @".\Art\greenCar.png";
        private const string _greenCarBroken = @".\Art\greenCarBroken.png";

        private const string _blueCar = @".\Art\blueCar.png";
        private const string _blueCarBroken = @".\Art\blueCarBroken.png";

        private const string _yellowCar = @".\Art\yellowCar.png";
        private const string _yellowCarBroken = @".\Art\yellowCarBroken.png";

        private const string _greyCar = @".\Art\greyCar.png";
        private const string _greyCarBroken = @".\Art\greyCarBroken.png";

        #endregion

        public static BitmapSource DrawTrack(Track track)
        {
            Bitmap b = RenderImage.SearchImageCache("empty");
            Graphics g = Graphics.FromImage(b);

            orientation = track.StartOrientation;

            if (Data.CurrentRace != null)
            {
                CheckTrackPosition(track);

                foreach (Section section in track.Sections)
                {
                    // Per sectietype wordt er iets anders geprint
                    // Elke type moet 8 regels printen van boven naar beneden
                    if (section.SectionType.Equals(Section.SectionTypes.RightCorner))
                    {
                        switch (orientation)
                        {
                            case 0:
                                g.DrawImage(Image.FromFile(_cornerLeftDown), cursorH, cursorV, 40, 40);
                                break;
                            case 1:
                                g.DrawImage(Image.FromFile(_cornerRightDown), cursorH, cursorV, 40, 40);
                                break;
                            case 2:
                                g.DrawImage(Image.FromFile(_cornerDownLeft), cursorH, cursorV, 40, 40);
                                break;
                            case 3:
                                g.DrawImage(Image.FromFile(_cornerDownRight), cursorH, cursorV, 40, 40);
                                break;
                        }

                        orientation++;
                        if (orientation > 3)
                        {
                            orientation = 0;
                        }

                    }
                    else if (section.SectionType.Equals(Section.SectionTypes.LeftCorner))
                    {
                        switch (orientation)
                        {
                            case 0:
                                g.DrawImage(Image.FromFile(_cornerRightDown), cursorH, cursorV, 40, 40);
                                break;
                            case 1:
                                g.DrawImage(Image.FromFile(_cornerDownLeft), cursorH, cursorV, 40, 40);
                                break;
                            case 2:
                                g.DrawImage(Image.FromFile(_cornerDownRight), cursorH, cursorV, 40, 40);
                                break;
                            case 3:
                                g.DrawImage(Image.FromFile(_cornerLeftDown), cursorH, cursorV, 40, 40);
                                break;
                        }

                        orientation--;
                        if (orientation < 0)
                        {
                            orientation = 3;
                        }

                    }
                    else if (section.SectionType.Equals(Section.SectionTypes.Straight))
                    {
                        switch (orientation)
                        {
                            case 0:
                                g.DrawImage(Image.FromFile(_straightVertical), cursorH, cursorV, 40, 40);
                                break;
                            case 1:
                                g.DrawImage(Image.FromFile(_straightHorizontal), cursorH, cursorV, 40, 40);
                                break;
                            case 2:
                                g.DrawImage(Image.FromFile(_straightVertical), cursorH, cursorV, 40, 40);
                                break;
                            case 3:
                                g.DrawImage(Image.FromFile(_straightHorizontal), cursorH, cursorV, 40, 40);
                                break;
                        }
                    }
                    else if (section.SectionType.Equals(Section.SectionTypes.Finish))
                    {
                        switch (orientation)
                        {
                            case 0:
                                g.DrawImage(Image.FromFile(_finishNorth), cursorH, cursorV, 40, 40);
                                break;
                            case 1:
                                g.DrawImage(Image.FromFile(_finishEast), cursorH, cursorV, 40, 40);
                                break;
                            case 2:
                                g.DrawImage(Image.FromFile(_finishSouth), cursorH, cursorV, 40, 40);
                                break;
                            case 3:
                                g.DrawImage(Image.FromFile(_finishWest), cursorH, cursorV, 40, 40);
                                break;
                        }
                    }
                    else if (section.SectionType.Equals(Section.SectionTypes.StartGrid))
                    {
                        switch (orientation)
                        {
                            case 0:
                                g.DrawImage(Image.FromFile(_startHorizontalNorth), cursorH, cursorV, 40, 40);
                                break;
                            case 1:
                                g.DrawImage(Image.FromFile(_startHorizontalEast), cursorH, cursorV, 40, 40);
                                break;
                            case 2:
                                g.DrawImage(Image.FromFile(_startHorizontalSouth), cursorH, cursorV, 40, 40);
                                break;
                            case 3:
                                g.DrawImage(Image.FromFile(_startHorizontalWest), cursorH, cursorV, 40, 40);
                                break;
                        }
                    }

                    // Geeft drivers in een sectie terug, array van maximaal 2 lang omdat er maar 2 drivers in een sectie kunnen zitten
                    IParticipant[] driversInSection = CheckForDrivers(section);

                    if (driversInSection[0] != null)
                    {
                        Bitmap bmp = (Bitmap)Bitmap.FromFile(getCar(driversInSection[0], true));

                        switch (orientation)
                        {
                            case 0:
                                bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                g.DrawImage(bmp, cursorH + 10, cursorV + 5, 8, 16);
                                break;
                            case 1:
                                g.DrawImage(bmp, cursorH + 10, cursorV + 10, 16, 8);
                                break;
                            case 2:
                                bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                g.DrawImage(bmp, cursorH + 10, cursorV + 10, 8, 16);
                                break;
                            case 3:
                                bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                g.DrawImage(bmp, cursorH + 5, cursorV + 10, 16, 8);
                                break;
                        }
                    }

                    if (driversInSection[1] != null)
                    {
                        Bitmap bmp = (Bitmap)Bitmap.FromFile(getCar(driversInSection[1], true));

                        switch (orientation)
                        {
                            case 0:
                                bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                g.DrawImage(bmp, cursorH + 20, cursorV + 5, 8, 16);
                                break;
                            case 1:
                                g.DrawImage(bmp, cursorH + 10, cursorV + 20, 16, 8);
                                break;
                            case 2:
                                bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                g.DrawImage(bmp, cursorH + 20, cursorV + 10, 8, 16);
                                break;
                            case 3:
                                bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                g.DrawImage(bmp, cursorH + 5, cursorV + 20, 16, 8);
                                break;
                        }
                    }

                    //Veranderd cursorH of cursorV, gekeken naaar de kant waar hij naartoe staat
                    if (orientation == 0)
                    {
                        cursorV -= 40;
                    }
                    else if (orientation == 1)
                    {
                        cursorH += 40;
                    }
                    else if (orientation == 2)
                    {
                        cursorV += 40;
                    }
                    else
                    {
                        cursorH -= 40;
                    }
                }
            }
            else
            {
                g.Clear(Color.Aqua);
                g.DrawString("Competition over", new Font("Arial", 16), new SolidBrush(Color.Gray), 0, 0);
                g.DrawString("The results:", new Font("Arial", 16), new SolidBrush(Color.Gray), 0, 25);
                g.DrawString("Driver\t\t\tPoints", new Font("Arial", 16), new SolidBrush(Color.Gray), 0, 50);

                var uitslag = from participant in Data.Competition?.Participants orderby participant.Points descending select participant;
                int y = 75;

                foreach (IParticipant participant in uitslag)
                {
                    if (participant.Name.Length < 8)
                    {

                        g.DrawString($"{participant.Name}\t\t\t{participant.Points}", new Font("Arial", 16), new SolidBrush(Color.Gray), 0, y);
                    }
                    else
                    {
                        g.DrawString($"{participant.Name}\t\t{participant.Points}", new Font("Arial", 16), new SolidBrush(Color.Gray), 0, y);
                    }
                    y += 25;
                }
                
            }

            return RenderImage.CreateBitmapSourceFromGdiBitmap(b);
        }

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
                    cursorV -= 40;
                }
                else if (orientation == 1)
                {
                    cursorH += 40;
                }
                else if (orientation == 2)
                {
                    cursorV += 40;
                }
                else
                {
                    cursorH -= 40;
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

            cursorH = laagsteX;
            cursorV = laagsteY;
        }

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

        public static string getCar(IParticipant p, bool countBroken)
        {
            bool broken = p.Car.IsBroken;

            switch (p.TeamColor)
                {
                case IParticipant.TeamColors.Red:
                    if (broken && countBroken) return _redCarBroken;
                    else return _redCar;
                case IParticipant.TeamColors.Green:
                    if (broken && countBroken) return _greenCarBroken;
                    else return _greenCar;
                case IParticipant.TeamColors.Blue:
                    if (broken && countBroken) return _blueCarBroken;
                    else return _blueCar;
                case IParticipant.TeamColors.Grey:
                    if (broken && countBroken) return _greyCarBroken;
                    else return _greyCar;
                case IParticipant.TeamColors.Yellow:
                    if (broken && countBroken) return _yellowCarBroken;
                    else return _yellowCar;
                }


            return null;
        }
    }
}
