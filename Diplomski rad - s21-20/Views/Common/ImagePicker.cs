using System.IO;
using System.Windows.Forms;

namespace Diplomski_rad___s21_20.Views.Common
{
    public static class ImagePicker
    {
        public static byte[] PickImageFromDisk()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Slike|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return File.ReadAllBytes(openFileDialog.FileName);
                }
            }

            return null;
        }
    }
}
