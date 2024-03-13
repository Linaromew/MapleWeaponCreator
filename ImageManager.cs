using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors.Transforms;
using System;
using System.IO;
using System.Numerics;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using SixLabors.ImageSharp.Formats.Tiff.Compression.Decompressors;

namespace MapleWeaponGen
{
    public enum weaponType
    {
        BAD_ENTRY,
        WEAPON_1H,
        WEAPON_2H,
        WEAPON_POLEARM,
        WEAPON_BOW,
        WEAPON_CROSSBOW,
        WEAPON_PISTOL,
        WEAPON_CLAW_KNUCKLE
    }

    public class ImageManager
    {
        private static string folderName = "";
        private static string inputFile = "";
        private static string baseFile = "base.png";
        private static string hdFile = "highRes.png";
        private static string hd2File = "highRes2.png";
        private static string hd3File = "highRes3.png";
        private static string hd4File = "highRes4.png";
        private static string hdSpritesheet = "rotHighResSheet.png";
        private static string spritesheet = "rotSheet.png";

        private static int rotationAngles = 72;

        private static weaponType selectedWeapon;

        public static void LoadImage(string inputPath, weaponType reqWeapon)
        {
            inputFile = inputPath;
            selectedWeapon = reqWeapon;

            using (var image = Image.Load<Rgba32>(inputPath))
            {
                if ((image.Width != 128) || (image.Height != 128))
                {
                    System.Windows.MessageBox.Show("Please provide a 128x128 PNG file, using one of the Sample Images as a reference.");
                    return;
                }
            }

            folderName = Path.GetRandomFileName();

            GenerateBaseImage(inputPath);
            GeneratedUpscaledImage();
        }

        private static void GenerateBaseImage(string inputPath)
        {
            using (var image = Image.Load<Rgba32>(inputPath))
            {
                PrepareImageAlpha(image);

                Directory.CreateDirectory(folderName);

                image.Save(Path.Combine(folderName + "/" + baseFile));
            }
        }

        private static void GeneratedUpscaledImage()
        {
            DoScaling();

            GenerateRotationSpritesheet();
            ScaleDownSpritesheetAddWeaponBorder();
            SliceSpritesheetIntoImages();
            Create90DegreeAngles();

            CleanupFiles();

            GenerateWeaponTypeFrames();
            GenerateWeaponXML();
        }

        private static void GenerateWeaponXML()
        {
            if (selectedWeapon == weaponType.WEAPON_1H)
            {
                File.Copy("weapon1H.img.xml", GetOutputPath("weapon.img.xml"));
            }

            else if (selectedWeapon == weaponType.WEAPON_2H)
            {
                File.Copy("weapon2H.img.xml", GetOutputPath("weapon.img.xml"));
            }

            else if (selectedWeapon == weaponType.WEAPON_POLEARM)
            {
                File.Copy("weaponPA.img.xml", GetOutputPath("weapon.img.xml"));
            }

            else if (selectedWeapon == weaponType.WEAPON_CROSSBOW)
            {
                File.Copy("weaponCrossbow.img.xml", GetOutputPath("weapon.img.xml"));
            }

            else if (selectedWeapon == weaponType.WEAPON_BOW)
            {
                File.Copy("weaponBow.img.xml", GetOutputPath("weapon.img.xml"));
            }

            Directory.SetCurrentDirectory(GetOutputPath(""));
            XMLHandling.SetupWeaponXML("weapon.img.xml");
            Directory.SetCurrentDirectory("..");
        }

        private static void GenerateWeaponTypeFrames()
        {
            switch (selectedWeapon)
            {
                case weaponType.WEAPON_1H:
                    VanillaFrameHandler.Do1HFrames();
                break;

                case weaponType.WEAPON_2H:
                    VanillaFrameHandler.Do2HFrames();
                break;

                case weaponType.WEAPON_POLEARM:
                    VanillaFrameHandler.DoPAFrames();
                break;

                case weaponType.WEAPON_CROSSBOW:
                    VanillaFrameHandler.DoCrossbowFrames();
                break;

                case weaponType.WEAPON_BOW:
                    VanillaFrameHandler.DoBowFrames();
                break;
            }

            //for (var i = 0; i < 120; i++)
            //{
            //    if (File.Exists(GetOutputPath($"{i}.png")))
            //    {
            //        File.Delete(GetOutputPath($"{i}.png"));
            //    }
            //}

            /* Generate rotations */
            var explorer = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "explorer.exe",
                    Arguments = Path.Combine(GetOutputPath(""))
                }
            };
            explorer.Start();
        }

        // In case rotationAngles is increased, this provides an easy way to adjust all old frames
        public static int GetFrameNumber(int input)
        {
            return input * (rotationAngles / 72);
        }

        private static void ScaleDownSpritesheetAddWeaponBorder()
        {
            using (var image = Image.Load<Rgba32>(GetOutputPath(hdSpritesheet)))
            {
                // Scale the image down
                image.Mutate(x => x.Resize(image.Width / 16, image.Height / 16, KnownResamplers.NearestNeighbor));

                RestoreWeaponBorder(image);

                image.Save(GetOutputPath(spritesheet));

                try { File.Delete(GetOutputPath(hdSpritesheet)); }
                catch { }
            }
        }



        private static void GenerateRotationSpritesheet()
        {
            string args = $"-r {rotationAngles} -f 0 -t 360 --columns 1 --width 2048 --height 2048 -o {hdSpritesheet} {hd4File}";
 
            /* Generate rotations */
            var procRotSprite = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "rotspr.exe",
                    WorkingDirectory = folderName,
                    Arguments = args
                }
            };
            procRotSprite.Start();
            procRotSprite.WaitForExit();
        }

        private static void SliceSpritesheetIntoImages()
        {
            using (var image = Image.Load<Rgba32>(GetOutputPath(spritesheet)))
            {
                var tileSize = new Size(128, 128);

                /* Iterate over the input image & extract tiles */
                for (int y = 0; y < image.Height; y += tileSize.Height)
                {
                    Console.WriteLine($"{y} : {image.Height}");

                    var clone = image.Clone(
                                i => i.Crop(new Rectangle(0, y, tileSize.Width, tileSize.Height))
                                .Resize(tileSize.Width, tileSize.Height));

                    var filename = (y / tileSize.Height) + ".png";
                    clone.Save(GetOutputPath(filename));
                }
            }
        }

        private static void Create90DegreeAngles()
        {
            using (var image = Image.Load<Rgba32>(inputFile))
            {
                // Iterate over the input image & extract tiles
                for (int y = 0; y < 4; y++)
                {
                    var clone = image.Clone(
                                i => i.Rotate(y * 90));

                    var filename = (y * (rotationAngles/4)) + ".png";
                    clone.Save(GetOutputPath(filename));
                }
            }
        }

        private static void CleanupFiles()
        {
            try { File.Delete(GetOutputPath(baseFile)); }
            catch { }

            try { File.Delete(GetOutputPath(hdFile)); }
            catch { }

            try { File.Delete(GetOutputPath(hd2File)); }
            catch { }

            try { File.Delete(GetOutputPath(hd3File)); }
            catch { }

            try { File.Delete(GetOutputPath(hd4File)); }
            catch { }

            try { File.Delete(GetOutputPath(spritesheet)); }
            catch { }
        }

        private static void DoScaling()
        {
            /* Apply Scale2x; input of baseFile, output of hdFile */
            var procScaleSprite = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "scalerx.exe",
                    WorkingDirectory = folderName,
                    Arguments = $"-k 2 {baseFile} {hdFile}"
                }
            };
            procScaleSprite.Start();
            procScaleSprite.WaitForExit();

            /* Apply Scale2x; input of hdFile, output of hdFile */
            var procScaleSprite2 = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "scalerx.exe",
                    WorkingDirectory = folderName,
                    Arguments = $"-k 2 {hdFile} {hd2File}"
                }
            };
            procScaleSprite2.Start();
            procScaleSprite2.WaitForExit();

            /* Apply Scale2x; input of hdFile, output of hdFile */
            var procScaleSprite3 = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "scalerx.exe",
                    WorkingDirectory = folderName,
                    Arguments = $"-k 2 {hd2File} {hd3File}"
                }
            };
            procScaleSprite3.Start();
            procScaleSprite3.WaitForExit();

            /* Apply Scale2x; input of hdFile, output of hdFile */
            var procScaleSprite4 = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "scalerx.exe",
                    WorkingDirectory = folderName,
                    Arguments = $"-k 2 {hd3File} {hd4File}"
                }
            };
            procScaleSprite4.Start();
            procScaleSprite4.WaitForExit();
        }

        public static string GetOutputPath(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return Path.GetFullPath(folderName);
            }
            return Path.GetFullPath(Path.Combine(folderName + "/" + input));
        }

        private static void PrepareImageAlpha(Image<Rgba32> image)
        {
            Rgba32 colourBlack = new Rgba32(0, 0, 0);
            Rgba32 colourMagenta = Color.Transparent;

            List<Point> replacePixels = new List<Point>();
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    /* If it's a transparent pixel, add it to the list of pixels to be changed */
                    if (image[x, y].A == 0)
                    {
                        replacePixels.Add(new Point(x, y));
                    }

                    /* If we have a black border pixel, add it to the list of pixels to be changed
                     * if it's on the outside of the weapon */
                    if (image[x, y] == colourBlack)
                    {
                        if (HasAlphaNeighbour(image, x, y))
                        {
                            replacePixels.Add(new Point(x, y));
                        }
                    }
                }
            }

            /* Change the pixels in the list to magenta */
            foreach (Point pixel in replacePixels)
            {
                image[pixel.X, pixel.Y] = colourMagenta;
            }
        }

        private static void RestoreWeaponBorder (Image<Rgba32> image)
        {
            Rgba32 colourBlack = new Rgba32(0, 0, 0);
            Rgba32 colourAlpha = Color.Transparent;

            List<Point> replacePixels = new List<Point>();
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    /* If it's a transparent pixel, add it to the list of pixels to be changed */
                    if (image[x, y] == colourAlpha)
                    {
                        if (HasNonAlphaNeighbour(image, x, y))
                        {
                            replacePixels.Add(new Point(x, y));
                        }
                    }
                }
            }

            /* Change the pixels in the list to magenta */
            foreach (Point pixel in replacePixels)
            {
                image[pixel.X, pixel.Y] = colourBlack;
            }
        }

        private static bool HasAlphaNeighbour(Image<Rgba32> image, int x, int y)
        {
            if (x > 0 && image[x - 1, y].A == 0)
                return true;
            if (x < image.Width - 1 && image[x + 1, y].A == 0)
                return true;
            if (y > 0 && image[x, y - 1].A == 0)
                return true;
            if (y < image.Height - 1 && image[x, y + 1].A == 0)
                return true;

            return false;
        }

        private static bool HasNonAlphaNeighbour(Image<Rgba32> image, int x, int y)
        {
            Rgba32 alpha = Color.Transparent;

            if (x > 0 && image[x - 1, y] != alpha)
                return true;
            if (x < image.Width - 1 && image[x + 1, y] != alpha)
                return true;
            if (y > 0 && image[x, y - 1] != alpha)
                return true;
            if (y < image.Height - 1 && image[x, y + 1] != alpha)
                return true;

            return false;
        }
    }
}