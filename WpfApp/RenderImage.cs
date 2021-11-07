using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Controller;
using Model;

namespace WpfApp
{
    public static class RenderImage
    {
        private static Dictionary<string, Bitmap> _imageCache = new Dictionary<string, Bitmap>();

        public static Bitmap SearchImageCache(string url)
        {
            if (!_imageCache.ContainsKey(url))
            {
                int[] dimensions = GetDimensions(Data.CurrentRace.Track);

                _imageCache[url] = GetBitmap(dimensions[0], dimensions[1]);
            }

            return (Bitmap)_imageCache[url].Clone();
        }

        public static Bitmap GetBitmap(int x, int y)
        {
            Bitmap b = new Bitmap(x, y);

            //Graphics g = Graphics.FromImage(b);
            //g.Clear(Color.Gray);

            return b;
        }

        public static int[] GetDimensions(Track track)
        {
            int[] dimensions = new int[2];

            int laagsteX = 0;
            int laagsteY = 0;
            int hoogsteX = 0;
            int hoogsteY = 0;

            int cursorH = 0;
            int cursorV = 0;
            int orientation = 1;

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

                if (cursorH > hoogsteX)
                {
                    hoogsteX = cursorH;
                }

                if (cursorV < laagsteY)
                {
                    laagsteY = cursorV;
                }

                if (cursorV > hoogsteY)
                {
                    hoogsteY = cursorV;
                }
            }

            // De lengte van de baan van links naar rechts, gemeten per sectie
            int lengteX = 0;
            for (int i = laagsteX; i < hoogsteX; i++)
            {
                lengteX++;
            }

            // De lengte van de baan van boven naar beneden, gemeten per sectie
            int lengteY = 0;
            for (int i = laagsteY; i < hoogsteY; i++)
            {
                lengteY++;
            }

            dimensions[0] = lengteX * 80;
            dimensions[1] = lengteY * 80;

            return dimensions;
        }

        public static void ClearCache()
        {
            _imageCache.Clear();
        }

        public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                var size = (rect.Width * rect.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }
    }
}
