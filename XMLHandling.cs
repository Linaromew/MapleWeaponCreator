using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Xml;

namespace MapleWeaponGen
{
    internal class XMLHandling
    {
        public static void SetupWeaponXML(string xmlFile)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("weapon.img.xml");

            XmlNode root = xmlDoc.SelectSingleNode("//imgdir[@name='weapon.img']");
            if (root != null)
            {
                foreach (XmlNode parentImgdir in root.ChildNodes)
                {
                    if (parentImgdir.Name == "imgdir")
                    {
                        string firstNodeName = parentImgdir.Attributes["name"].Value;

                        foreach (XmlNode innerImgdir in parentImgdir.ChildNodes)
                        {
                            if (innerImgdir.Name == "imgdir")
                            {
                                string secondNodeName = innerImgdir.Attributes["name"].Value;

                                XmlNode canvasNode = innerImgdir.SelectSingleNode("canvas[@name='weapon']");

                                if (canvasNode != null)
                                {
                                    string input = $"{firstNodeName}.{secondNodeName}.weapon.png";
                                    Console.WriteLine(input);

                                    if (File.Exists(input))
                                    {
                                        Console.WriteLine($"Found image!");
                                        XmlAttribute baseDataAttr = canvasNode.Attributes["basedata"];

                                        if (baseDataAttr != null)
                                        {
                                            var base64 = ConvertImageToBase64(input);

                                            XmlAttribute widthAttr = canvasNode.Attributes["width"];
                                            XmlAttribute heightAttr = canvasNode.Attributes["height"];

                                            baseDataAttr.Value = base64.Base64Data;
                                            widthAttr.Value = base64.Width.ToString();
                                            heightAttr.Value = base64.Height.ToString();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                xmlDoc.Save(xmlFile);

                string dir = "Raw";

                Directory.CreateDirectory(dir);
                foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.weapon.png"))
                {
                    File.Move(file, Path.Combine($"Raw/{Path.GetFileName(file)}"));
                }
            }
        }

        private static (string Base64Data, int Width, int Height) ConvertImageToBase64(string imagePath)
        {
            using (Image image = Image.FromFile(imagePath))
            {
                int width = image.Width;
                int height = image.Height;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Save the image in the desired format (e.g., JPEG, PNG)
                    image.Save(memoryStream, ImageFormat.Png);

                    // Convert the image bytes to a base64 string
                    byte[] imageBytes = memoryStream.ToArray();
                    string base64Image = Convert.ToBase64String(imageBytes);

                    return (base64Image, width, height);
                }
            }
        }
    }
}
