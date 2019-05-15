using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using Alturos.Yolo;
using Alturos.Yolo.Model;

namespace DetectionConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Image myImage = Image.FromFile(args[0]);
            Image yoloImage = myImage;
            var detectorConf = new ConfigurationDetector();
            var conf = detectorConf.Detect();
            var yolo = new YoloWrapper(conf);
            var memStream = new System.IO.MemoryStream();
            yoloImage.Save(memStream, ImageFormat.Png);
            var _items = yolo.Detect(memStream.ToArray()).ToList();
            SaveYoloImage(yoloImage, _items);
        }

        static void SaveYoloImage(Image img, List<YoloItem> items)
        {
            var font = new Font("Tahoma", 15, FontStyle.Regular);
            var brush = new SolidBrush(Color.Blue);

            var graphics = Graphics.FromImage(img);
            foreach (var item in items)
            {
                var x = item.X;
                var y = item.Y;
                var width = item.Width;
                var height = item.Height;

                var rect = new Rectangle(x, y, width, height);
                var pen = new Pen(Color.Blue, 4);
                var point = new Point(x , y - 25);

                graphics.DrawRectangle(pen, rect);
                graphics.DrawString(item.Type, font, brush, point);
            }
            img.Save("yolo_"+DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss")+".jpg");
        }
    }
}