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

            Directory.SetCurrentDirectory(GetOutputPath(""));
            XMLHandling.SetupWeaponXML("weapon.img.xml");
            Directory.SetCurrentDirectory("..");
        }

        private static void GenerateWeaponTypeFrames()
        {
            var noRemove = new List<int> { };
            if (selectedWeapon == weaponType.WEAPON_1H)
            {
                noRemove = new List<int> { 0, 3, 9, 15, 21, 27, 30, 39, 51, 69 };
            }

            else if (selectedWeapon == weaponType.WEAPON_2H)
            {
                noRemove = new List<int> { 0, 9, 15, 18, 21, 27, 33, 69 };
            }

            else if (selectedWeapon == weaponType.WEAPON_POLEARM)
            {
                noRemove = new List<int> { 0, 3, 9, 15, 21, 26, 27, 28, 33, 63, 69 };
            }

            for (var i = 0; i < 120; i++)
            {
                if (noRemove.Contains(i)) continue;

                if (File.Exists(GetOutputPath($"{i}.png")))
                {
                    File.Delete(GetOutputPath($"{i}.png"));
                }
            }

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

            System.Windows.MessageBox.Show("Please make any manual edit to the produced images now.\r\n\r\nPress OK to finish building the weapon XML data.");

            if (selectedWeapon == weaponType.WEAPON_1H)
            {
                Do1HFrames();
            }

            else if (selectedWeapon == weaponType.WEAPON_2H)
            {
                Do2HFrames();
            }

            else if (selectedWeapon == weaponType.WEAPON_POLEARM)
            {
                DoPAFrames();
            }

            for (var i = 0; i < 120; i++)
            {
                if (File.Exists(GetOutputPath($"{i}.png")))
                {
                    File.Delete(GetOutputPath($"{i}.png"));
                }
            }
        }

        // In case rotationAngles is increased, this provides an easy way to adjust all old frames
        private static int GetFrameNumber(int input)
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

        private static void Do1HFrames()
        {
            using (var image = Image.Load<Rgba32>(GetOutputPath($"{ GetFrameNumber(0)}.png")))
            {
                image.Mutate(x => x.Crop(new Rectangle(19, 23, 92, 81)));

                var horiFrames = new List<string> { "proneStab.0.weapon.png", "proneStab.1.weapon.png", "stabO1.0.weapon.png", "stabO1.1.weapon.png", "stabO2.0.weapon.png", "stabO2.1.weapon.png", "stabOF.0.weapon.png", "stabOF.1.weapon.png", "stabOF.2.weapon.png", "walk1.0.weapon.png", "walk1.2.weapon.png", "swingO3.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{ GetFrameNumber(9)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(21, 21, 86, 86))
                .Flip(FlipMode.Horizontal));

                var horiFrames = new List<string> { "swingO1.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{ GetFrameNumber(18)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(26, 24, 79, 94)));

                var horiFrames = new List<string> { "shoot1.0.weapon.png", "shoot1.1.weapon.png", "shootF.0.weapon.png", "shootF.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(0)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(17, 24, 93, 81))
                .Flip(FlipMode.Horizontal));

                var horiFrames = new List<string> { "swingO1.0.weapon.png", "swingO3.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(69)}.png")))
            {
                image.Mutate(x => x.Crop(new Rectangle(17, 27, 93, 80)));

                var horiFrames = new List<string> { "stand1.0.weapon.png", "walk1.3.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(21)}.png")))
            {
                image.Mutate(x => x.Crop(new Rectangle(28, 18, 80, 93)));

                var horiFrames = new List<string> { "fly.0.weapon.png", "jump.0.weapon.png", "swingO2.1.weapon.png", "swingOF.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(15)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(32, 19, 73, 90))
                .Flip(FlipMode.Horizontal));

                var horiFrames = new List<string> { "swingO3.0.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }


            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(27)}.png")))
            {
                image.Mutate(x => x.Crop(new Rectangle(23, 21, 86, 86)));

                var horiFrames = new List<string> { "swingO2.0.weapon.png", "swingOF.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(3)}.png")))
            {
                image.Mutate(x => x.Crop(new Rectangle(18, 21, 93, 80)));

                var horiFrames = new List<string> { "alert.0.weapon.png", "alert.1.weapon.png", "alert.2.weapon.png", "walk1.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(51)}.png")))
            {
                image.Mutate(x => x.Crop(new Rectangle(27, 18, 80, 93)));

                var horiFrames = new List<string> { "swingO2.2.weapon.png", "swingOF.0.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(39)}.png")))
            {
                image.Mutate(x => x.Crop(new Rectangle(18, 28, 93, 80)));

                var horiFrames = new List<string> { "swingO1.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(30)}.png")))
            {
                image.Mutate(x => x.Crop(new Rectangle(19, 23, 93, 80)));

                var horiFrames = new List<string> { "swingOF.3.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }
        }

        private static void Do2HFrames()
        {
            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(0)}.png")))
            {
                image.Mutate(x => x.Crop(new Rectangle(10, 21, 108, 86)));

                var horiFrames = new List<string> { "proneStab.0.weapon.png", "proneStab.1.weapon.png", "stabO1.0.weapon.png", "stabO1.1.weapon.png", "stabO2.0.weapon.png", "stabO2.1.weapon.png", "stabOF.0.weapon.png", "stabOF.1.weapon.png", "stabOF.2.weapon.png", };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(0)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(10, 21, 108, 86))
                .Flip(FlipMode.Horizontal));

                var horiFrames = new List<string> { "swingT3.0.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(9)}.png")))
            {
                image.Mutate(x => x.Crop(new Rectangle(11, 15, 100, 96)));

                var horiFrames = new List<string> { "alert.0.weapon.png", "alert.1.weapon.png", "alert.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(27)}.png")))
            {
                image.Mutate(x => x.Crop(new Rectangle(18, 13, 98, 98)));

                var horiFrames = new List<string> { "fly.0.weapon.png", "jump.0.weapon.png", "stand2.0.weapon.png", "swingTF.2.weapon.png", "walk2.0.weapon.png", "walk2.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(15)}.png")))
            {
                image.Mutate(x => x.Crop(new Rectangle(18, 10, 86, 108)));

                var horiFrames = new List<string> { "swingT2.1.weapon.png", "swingT3.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(21)}.png")))
            {
                image.Mutate(x => x.Crop(new Rectangle(25, 10, 86, 108)));

                var horiFrames = new List<string> { "swingT1.1.weapon.png", "swingT2.0.weapon.png", "swingTF.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(69)}.png")))
            {
                image.Mutate(x => x.Crop(new Rectangle(10, 25, 108, 86)));

                var horiFrames = new List<string> { "swingT1.2.weapon.png", "swingT2.2.weapon.png", "swingTF.3.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(18)}.png")))
            {
                image.Mutate(x => x.Crop(new Rectangle(21, 9, 86, 108)));

                var horiFrames = new List<string> { "swingTF.0.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(27)}.png")))
            {
                image.Mutate(x => x.Crop(new Rectangle(18, 14, 100, 96)));

                var horiFrames = new List<string> { "walk2.3.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(27)}.png")))
            {
                image.Mutate(x => x.Crop(new Rectangle(19, 11, 96, 100)));

                var horiFrames = new List<string> { "walk2.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(33)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(12, 24, 108, 86))
                .Flip(FlipMode.Horizontal));

                var horiFrames = new List<string> { "swingT3.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(33)}.png")))
            {
                image.Mutate(x => x.Crop(new Rectangle(11, 18, 108, 86)));

                var horiFrames = new List<string> { "swingT1.0.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }
        }

        private static void DoPAFrames()
        {
            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(9)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(23, 24, 88, 83)));

                var horiFrames = new List<string> { "alert.0.weapon.png", "alert.1.weapon.png", "alert.2.weapon.png"};
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(27)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(22, 26, 83, 84)));

                var horiFrames = new List<string> { "fly.0.weapon.png", "jump.0.weapon.png", "stand2.1.weapon.png", "swingP2.0.weapon.png", "swingT2.0.weapon.png", "walk2.0.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(0)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(13, 42, 111, 42)));

                var horiFrames = new List<string> { "proneStab.0.weapon.png", "proneStab.1.weapon.png", "stabT1.2.weapon.png", "swingPF.0.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(69)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(19, 44, 106, 48)));

                var horiFrames = new List<string> { "stabT1.0.weapon.png", "stabT1.1.weapon.png", "stabT2.0.weapon.png", "stabT2.1.weapon.png", "swingP1.2.weapon.png", "swingP2.2.weapon.png", "swingPF.1.weapon.png", "swingPF.3.weapon.png", "swingT2.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(3)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(15, 34, 108, 47)));

                var horiFrames = new List<string> { "stabT2.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(63)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(16, 19, 91, 86)));

                var horiFrames = new List<string> { "stabTF.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(33)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(4, 37, 106, 48)));

                var horiFrames = new List<string> { "swingP1.0.weapon.png", "swingPF.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(21)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(48, 17, 47, 108)));

                var horiFrames = new List<string> { "swingP1.1.weapon.png", "swingP2.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(15)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(36, 16, 48, 106)));

                var horiFrames = new List<string> { "swingT2.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(26)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(23, 26, 82, 88)));

                var horiFrames = new List<string> { "stand2.0.weapon.png", "walk2.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(GetOutputPath($"{GetFrameNumber(28)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(18, 25, 83, 84)));

                var horiFrames = new List<string> { "stand2.2.weapon.png", "walk2.3.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(GetOutputPath(frame));
                }
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

                // Iterate over the input image & extract tiles
                for (int y = 0; y < image.Height; y += tileSize.Height)
                {
                    Console.WriteLine($"{y} : {image.Height}");

                    var clone = image.Clone(
                                i => i.Crop(new Rectangle(0, y, tileSize.Width, tileSize.Height))
                                .Resize(tileSize.Width, tileSize.Height));

                    // Save!
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

                    // Save!
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

        private static string GetOutputPath(string input)
        {
            return Path.Combine(folderName + "/" + input);
        }

        private static void PrepareImageAlpha(Image<Rgba32> image)
        {
            Rgba32 colourBlack = new Rgba32(0, 0, 0);
            Rgba32 colourMagenta = Color.Transparent;

            List<Point> replacePixels = new List<Point>(); // create a list to store pixels to be changed

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

            List<Point> replacePixels = new List<Point>(); // create a list to store pixels to be changed

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
            Rgba32 alpha = Color.Transparent;

            if (x > 0 && image[x - 1, y] == alpha)
                return true;
            if (x < image.Width - 1 && image[x + 1, y] == alpha)
                return true;
            if (y > 0 && image[x, y - 1] == alpha)
                return true;
            if (y < image.Height - 1 && image[x, y + 1] == alpha)
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

        private static bool IsNewBorderPixel(Image<Rgba32> image, int x, int y)
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
    }
}