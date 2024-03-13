using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;

namespace MapleWeaponGen
{
    internal static class VanillaFrameHandler
    {
        #region 1H
        public static void Do1HFrames()
        {
            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(0)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(19, 23, 92, 81)));

                var horiFrames = new List<string> { "proneStab.0.weapon.png", "proneStab.1.weapon.png", "stabO1.0.weapon.png", "stabO1.1.weapon.png", "stabO2.0.weapon.png", "stabO2.1.weapon.png", "stabOF.0.weapon.png", "stabOF.1.weapon.png", "stabOF.2.weapon.png", "walk1.0.weapon.png", "walk1.2.weapon.png", "swingO3.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(9)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(21, 21, 86, 86))
                .Flip(FlipMode.Horizontal));

                var horiFrames = new List<string> { "swingO1.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(18)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(26, 24, 79, 94)));

                var horiFrames = new List<string> { "shoot1.0.weapon.png", "shoot1.1.weapon.png", "shootF.0.weapon.png", "shootF.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(0)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(17, 24, 93, 81))
                .Flip(FlipMode.Horizontal));

                var horiFrames = new List<string> { "swingO1.0.weapon.png", "swingO3.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(69)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(17, 27, 93, 80)));

                var horiFrames = new List<string> { "stand1.0.weapon.png", "walk1.3.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(21)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(28, 18, 80, 93)));

                var horiFrames = new List<string> { "fly.0.weapon.png", "jump.0.weapon.png", "swingO2.1.weapon.png", "swingOF.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(15)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(32, 19, 73, 90))
                .Flip(FlipMode.Horizontal));

                var horiFrames = new List<string> { "swingO3.0.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }


            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(27)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(23, 21, 86, 86)));

                var horiFrames = new List<string> { "swingO2.0.weapon.png", "swingOF.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(3)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(18, 21, 93, 80)));

                var horiFrames = new List<string> { "alert.0.weapon.png", "alert.1.weapon.png", "alert.2.weapon.png", "walk1.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(51)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(27, 18, 80, 93)));

                var horiFrames = new List<string> { "swingO2.2.weapon.png", "swingOF.0.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(39)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(18, 28, 93, 80)));

                var horiFrames = new List<string> { "swingO1.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(30)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(19, 23, 93, 80)));

                var horiFrames = new List<string> { "swingOF.3.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }
        }
        #endregion 1H
        #region 2H
        public static void Do2HFrames()
        {
            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(0)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(10, 21, 108, 86)));

                var horiFrames = new List<string> { "proneStab.0.weapon.png", "proneStab.1.weapon.png", "stabO1.0.weapon.png", "stabO1.1.weapon.png", "stabO2.0.weapon.png", "stabO2.1.weapon.png", "stabOF.0.weapon.png", "stabOF.1.weapon.png", "stabOF.2.weapon.png", };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(0)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(10, 21, 108, 86))
                .Flip(FlipMode.Horizontal));

                var horiFrames = new List<string> { "swingT3.0.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(9)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(11, 15, 100, 96)));

                var horiFrames = new List<string> { "alert.0.weapon.png", "alert.1.weapon.png", "alert.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(27)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(18, 13, 98, 98)));

                var horiFrames = new List<string> { "fly.0.weapon.png", "jump.0.weapon.png", "stand2.0.weapon.png", "swingTF.2.weapon.png", "walk2.0.weapon.png", "walk2.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(15)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(18, 10, 86, 108)));

                var horiFrames = new List<string> { "swingT2.1.weapon.png", "swingT3.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(21)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(25, 10, 86, 108)));

                var horiFrames = new List<string> { "swingT1.1.weapon.png", "swingT2.0.weapon.png", "swingTF.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(69)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(10, 25, 108, 86)));

                var horiFrames = new List<string> { "swingT1.2.weapon.png", "swingT2.2.weapon.png", "swingTF.3.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(69)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(10, 25, 108, 86))
                .Flip(FlipMode.Horizontal));

                var horiFrames = new List<string> { "swingT3.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(18)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(21, 9, 86, 108)));

                var horiFrames = new List<string> { "swingTF.0.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(27)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(18, 14, 100, 96)));

                var horiFrames = new List<string> { "walk2.3.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(27)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(19, 11, 96, 100)));

                var horiFrames = new List<string> { "walk2.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(33)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(11, 18, 108, 86)));

                var horiFrames = new List<string> { "swingT1.0.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(69)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(10, 25, 108, 86))
                .Flip(FlipMode.Horizontal));

                var horiFrames = new List<string> { "swingOF.3.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }
        }
        #endregion 2H
        #region PA
        public static void DoPAFrames()
        {
            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(9)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(23, 24, 88, 83)));

                var horiFrames = new List<string> { "alert.0.weapon.png", "alert.1.weapon.png", "alert.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(27)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(22, 26, 83, 84)));

                var horiFrames = new List<string> { "fly.0.weapon.png", "jump.0.weapon.png", "stand2.1.weapon.png", "swingP2.0.weapon.png", "swingT2.0.weapon.png", "walk2.0.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(0)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(13, 42, 111, 42)));

                var horiFrames = new List<string> { "proneStab.0.weapon.png", "proneStab.1.weapon.png", "stabT1.2.weapon.png", "swingPF.0.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(69)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(19, 44, 106, 48)));

                var horiFrames = new List<string> { "stabT1.0.weapon.png", "stabT1.1.weapon.png", "stabT2.0.weapon.png", "stabT2.1.weapon.png", "swingP1.2.weapon.png", "swingP2.2.weapon.png", "swingPF.1.weapon.png", "swingPF.3.weapon.png", "swingT2.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(3)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(15, 34, 108, 47)));

                var horiFrames = new List<string> { "stabT2.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(63)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(16, 19, 91, 86)));

                var horiFrames = new List<string> { "stabTF.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(33)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(4, 37, 106, 48)));

                var horiFrames = new List<string> { "swingP1.0.weapon.png", "swingPF.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(21)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(48, 17, 47, 108)));

                var horiFrames = new List<string> { "swingP1.1.weapon.png", "swingP2.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(15)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(36, 16, 48, 106)));

                var horiFrames = new List<string> { "swingT2.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(26)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(23, 26, 82, 88)));

                var horiFrames = new List<string> { "stand2.0.weapon.png", "walk2.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(28)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(18, 25, 83, 84)));

                var horiFrames = new List<string> { "stand2.2.weapon.png", "walk2.3.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(69)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(19, 44, 106, 48))
                .Flip(FlipMode.Horizontal));

                var horiFrames = new List<string> { "swingOF.3.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }
        }
        #endregion PA
        #region Crossbow
        public static void DoCrossbowFrames()
        {
            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(9)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(23, 24, 88, 83)));

                var horiFrames = new List<string> { "alert.0.weapon.png", "alert.1.weapon.png", "alert.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(27)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(22, 26, 83, 84)));

                var horiFrames = new List<string> { "fly.0.weapon.png", "jump.0.weapon.png", "stand2.1.weapon.png", "swingP2.0.weapon.png", "swingT2.0.weapon.png", "walk2.0.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(0)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(13, 42, 111, 42)));

                var horiFrames = new List<string> { "proneStab.0.weapon.png", "proneStab.1.weapon.png", "stabT1.2.weapon.png", "swingPF.0.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(69)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(19, 44, 106, 48)));

                var horiFrames = new List<string> { "stabT1.0.weapon.png", "stabT1.1.weapon.png", "stabT2.0.weapon.png", "stabT2.1.weapon.png", "swingP1.2.weapon.png", "swingP2.2.weapon.png", "swingPF.1.weapon.png", "swingPF.3.weapon.png", "swingT2.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(69)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(19, 44, 106, 48))
                .Flip(FlipMode.Horizontal));

                var horiFrames = new List<string> { "swingOF.3.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(3)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(15, 34, 108, 47)));

                var horiFrames = new List<string> { "stabT2.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(63)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(16, 19, 91, 86)));

                var horiFrames = new List<string> { "stabTF.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(33)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(4, 37, 106, 48)));

                var horiFrames = new List<string> { "swingP1.0.weapon.png", "swingPF.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(21)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(48, 17, 47, 108)));

                var horiFrames = new List<string> { "swingP1.1.weapon.png", "swingP2.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(15)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(36, 16, 48, 106)));

                var horiFrames = new List<string> { "swingT2.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(26)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(23, 26, 82, 88)));

                var horiFrames = new List<string> { "stand2.0.weapon.png", "walk2.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(28)}.png")))
            {
                image.Mutate(x => x
                .Crop(new Rectangle(18, 25, 83, 84)));

                var horiFrames = new List<string> { "stand2.2.weapon.png", "walk2.3.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }
        }
        #endregion Crossbow
        #region Bow
        public static void DoBowFrames()
        {
            var imagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sample Images", "Bow Strings");

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(3)}.png")))
            {
                var bowString = Image.Load<Rgba32>(imagesFolder + "/" + "bowString-3.png");

                image.Mutate(x => x
                .DrawImage(bowString, 1)
                .Crop(new Rectangle(24, 10, 88, 113)));

                var horiFrames = new List<string> { "alert.0.weapon.png", "alert.1.weapon.png", "alert.2.weapon.png", "fly.0.weapon.png", "jump.0.weapon.png", "swingT1.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            /* Shoot frames */
            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(0)}.png")))
            {
                var bowString = Image.Load<Rgba32>(imagesFolder + "/" + "bowString-Active.png");

                image.Mutate(x => x
                .DrawImage(bowString, 1)
                .Crop(new Rectangle(25, 7, 80, 114)));

                var horiFrames = new List<string> { "shoot1.1.weapon.png", "shootF.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(0)}.png")))
            {
                var bowString = Image.Load<Rgba32>(imagesFolder + "/" + "bowString-0.png");

                image.Mutate(x => x
                .DrawImage(bowString, 1)
                .Crop(new Rectangle(25, 7, 80, 114)));

                var horiFrames = new List<string> { "shoot1.0.weapon.png", "shootF.0.weapon.png", "shootF.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(18)}.png")))
            {
                var bowString = Image.Load<Rgba32>(imagesFolder + "/" + "bowString-18.png");

                image.Mutate(x => x
                .DrawImage(bowString, 1)
                .Crop(new Rectangle(7, 25, 114, 80)));

                var horiFrames = new List<string> { "swingT1.0.weapon.png", "proneStab.0.weapon.png", "proneStab.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(33)}.png")))
            {
                var bowString = Image.Load<Rgba32>(imagesFolder + "/" + "bowString-33.png");

                image.Mutate(x => x
                .DrawImage(bowString, 1)
                .Crop(new Rectangle(17, 10, 88, 113)));

                var horiFrames = new List<string> { "swingT3.2.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(51)}.png")))
            {
                var bowString = Image.Load<Rgba32>(imagesFolder + "/" + "bowString-51.png");

                image.Mutate(x => x
                .DrawImage(bowString, 1)
                .Crop(new Rectangle(6, 17, 112, 88)));

                var horiFrames = new List<string> { "swingT1.2.weapon.png", "swingT3.0.weapon.png", "swingOF.3.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(54)}.png")))
            {
                var bowString = Image.Load<Rgba32>(imagesFolder + "/" + "bowString-54.png");

                image.Mutate(x => x
                .DrawImage(bowString, 1)
                 .Crop(new Rectangle(7, 25, 114, 80)));
                //.Crop(new Rectangle(7, 23, 114, 80)));

                var horiFrames = new List<string> { "stand1.0.weapon.png", "stand1.1.weapon.png", "stand1.2.weapon.png", "swingT3.1.weapon.png", "walk1.0.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }

            using (var image = Image.Load<Rgba32>(ImageManager.GetOutputPath($"{ImageManager.GetFrameNumber(57)}.png")))
            {
                var bowString = Image.Load<Rgba32>(imagesFolder + "/" + "bowString-57.png");

                image.Mutate(x => x
                .DrawImage(bowString, 1)
                .Crop(new Rectangle(10, 18, 112, 88)));

                var horiFrames = new List<string> { "walk1.1.weapon.png" };
                foreach (var frame in horiFrames)
                {
                    image.Save(ImageManager.GetOutputPath(frame));
                }
            }
        }
        #endregion Bow
    }
}