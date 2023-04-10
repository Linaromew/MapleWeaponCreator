using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MapleWeaponGen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PrevDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
                e.Handled = true;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void PrevDragDrop(object sender, DragEventArgs e)
        {
            if (null != e.Data && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Handled = true;

                string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                foreach (string file in fileList)
                {
                    BaseImagePath.Text = file;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string inputFile = BaseImagePath.Text;

            var reqWeapon = weaponType.BAD_ENTRY;

            if ((bool)wep1H.IsChecked) { reqWeapon = weaponType.WEAPON_1H; }
            if ((bool)wep2H.IsChecked) { reqWeapon = weaponType.WEAPON_2H; }
            if ((bool)wepPA.IsChecked) { reqWeapon = weaponType.WEAPON_POLEARM; }
            
            if (reqWeapon == weaponType.BAD_ENTRY)
            {
                MessageBox.Show("Please select a valid weapon type.");
                return;
            }


            if (!string.IsNullOrWhiteSpace(inputFile))
            {
                if (File.Exists(inputFile))
                {
                    ImageManager.LoadImage(inputFile, reqWeapon);
                    MessageBox.Show("Done!");
                }
            }
            else
            {
                MessageBox.Show("Please provide a 128x128 PNG file, using one of the Sample Images as a reference.");
                return;
            }
        }
    }
}

