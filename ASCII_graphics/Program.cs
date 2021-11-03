using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ASCII_graphics
{
    class Program
    {
        private const double WIDTH_OFFSET = 1.5;

        [STAThread]

        static void Main(string[] args)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Images | *.bmp; *.png; *.jpg; *.JPEG"
            };

            Console.WriteLine("Press enter to start...\n");
            while (true)
            {
                Console.ReadLine();

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    continue;

                Console.Clear();

                var bitmap = new Bitmap(openFileDialog.FileName);
                bitmap = ResizeBitmap(bitmap);
                bitmap.ToGrayscale();

                var converter = new BitmapToASCIIConverter(bitmap);
                var rows = converter.Convert();

                foreach (var row in rows)
                    Console.WriteLine(row);

                Console.SetCursorPosition(0, 0);

            }
        }

        private static Bitmap ResizeBitmap(Bitmap bitmap)
        {
            var maxWidth = 350;
            var newHeigh = bitmap.Height / WIDTH_OFFSET * maxWidth / bitmap.Width;
            if (bitmap.Width > maxWidth || bitmap.Height > newHeigh)
                bitmap = new Bitmap(bitmap, new Size(maxWidth, (int)newHeigh));
            return bitmap;
        }
    }
}
