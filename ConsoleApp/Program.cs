using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            img.Image = Image.FromFile(fileD.FileName);
            var detectorConf = new ConfigurationDetector();
            var conf = detectorConf.Detect();
            var yolo = new YoloWrapper(conf);
            var memStream = new System.IO.MemoryStream();
            img.Image.Save(memStream, ImageFormat.Png);
            var _items = yolo.Detect(memStream.ToArray()).ToList();
            AddBoxes(img, _items);
        }

        void AddBoxes(PictureBox box, List<YoloItem> items)
        {
            var img = box.Image;

            var font = new Font("Arial", 30, FontStyle.Bold);
            var brush = new SolidBrush(Color.LightGreen);

            var graphics = Graphics.FromImage(img);
            foreach (var item in items)
            {
                var x = item.X;
                var y = item.Y;
                var width = item.Width;
                var height = item.Height;

                var rect = new Rectangle(x, y, width, height);
                var pen = new Pen(Color.LightBlue, 6);
                var point = new Point(x, y);

                graphics.DrawRectangle(pen, rect);
                graphics.DrawString(item.Type, font, brush, point);
            }
            box.Image = img;
            Console.WriteLine("Hello World!");
        }
    }
}