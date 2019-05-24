using Alturos.Yolo;
using Alturos.Yolo.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObjectDetection
{
    public partial class Form1 : Form
    {
        YoloWrapper yoloWrapper = new
        YoloWrapper("yolov3/yolov3.cfg", "yolov3/yolov3.weights", "yolov3/coco.names");
        //YoloWrapper("yolov3-tiny/yolov3-tiny.cfg", "yolov3-tiny/yolov3.weights", "yolov3-tiny/coco.names");
        //YoloWrapper("yolov2/yolov2-tiny-voc.cfg", "yolov2/yolov2-tiny-voc.weights", "yolov2/voc.names");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fileD = new OpenFileDialog();
            fileD.Filter = "Image Files|*.jpg;*.png";
            if (fileD.ShowDialog() == DialogResult.OK)
            {
                img.Image = Image.FromFile(fileD.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var memStream = new System.IO.MemoryStream();
            img.Image.Save(memStream, ImageFormat.Png);
            var _items = this.yoloWrapper.Detect(memStream.ToArray()).ToList();
            AddBoxes(img, _items);
        }
        void AddBoxes(PictureBox box, List<YoloItem> items)
        {
            var img = box.Image;
            var brush = new SolidBrush(Color.Blue);

            var graphics = Graphics.FromImage(img);
            foreach (var item in items)
            {
                var x = item.X;
                var y = item.Y;
                var width = item.Width;
                var height = item.Height;

                var rect = new Rectangle(x, y, width, height);
                var pen = new Pen(Color.Blue,4);

                graphics.DrawRectangle(pen,rect);

                //Label Box
                var labelfont = new Font("Arial", 20, FontStyle.Bold);

                SizeF stringSize = new SizeF();
                stringSize = graphics.MeasureString(item.Type, labelfont);
                // Draw rectangle representing size of string.
                graphics.DrawRectangle(new Pen(Color.Blue, 2), x, y - stringSize.Height - 2, stringSize.Width, stringSize.Height);
                graphics.FillRectangle(Brushes.Blue, x, y - stringSize.Height - 2, stringSize.Width, stringSize.Height);
                // Draw string to screen.
                graphics.DrawString(item.Type, labelfont, Brushes.White, new PointF(x, y - stringSize.Height -2));
            }
            box.Image = img;
        }
    }
}
