using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace MapleWeaponGen
{
    public static class Scale2xAlgorithm
    {
        public static Bitmap Scale2x(Bitmap original)
        {
            Bitmap scaled = new Bitmap(original.Width << 1, original.Height << 1);

            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color p = original.GetPixel(x, y);
                    Color a = original.GetPixel(x, y - 1 >= 0 ? y - 1 : y);
                    Color b = original.GetPixel(x + 1 < original.Width ? x + 1 : x, y);
                    Color c = original.GetPixel(x - 1 >= 0 ? x - 1 : x, y);
                    Color d = original.GetPixel(x, y + 1 < original.Height ? y + 1 : y);

                    if (c.ToArgb() == a.ToArgb() && c.ToArgb() != d.ToArgb() && a.ToArgb() != b.ToArgb())
                    {
                        scaled.SetPixel((x << 1), (y << 1), a);
                    }
                    else
                    {
                        scaled.SetPixel((x << 1), (y << 1), p);
                    }

                    if (a.ToArgb() == b.ToArgb() && a.ToArgb() != c.ToArgb() && b.ToArgb() != d.ToArgb())
                    {
                        scaled.SetPixel((x << 1) + 1, (y << 1), b);
                    }
                    else
                    {
                        scaled.SetPixel((x << 1) + 1, (y << 1), p);
                    }

                    if (d.ToArgb() == c.ToArgb() && d.ToArgb() != b.ToArgb() && c.ToArgb() != a.ToArgb())
                    {
                        scaled.SetPixel((x << 1), (y << 1) + 1, c);
                    }
                    else
                    {
                        scaled.SetPixel((x << 1), (y << 1) + 1, p);
                    }

                    if (b.ToArgb() == d.ToArgb() && b.ToArgb() != a.ToArgb() && d.ToArgb() != c.ToArgb())
                    {
                        scaled.SetPixel((x << 1) + 1, (y << 1) + 1, d);
                    }
                    else
                    {
                        scaled.SetPixel((x << 1) + 1, (y << 1) + 1, p);
                    }
                }
            }

            return scaled;
        }
    }
}