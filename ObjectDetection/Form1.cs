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
        YoloWrapper("yolov3/yolov3.cfg", "yolov3/yolov3.weights", "yolov3/names.coco");
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

            var font = new Font("Arial", 30, FontStyle.Bold);
            var brush = new SolidBrush(Color.Blue);

            var graphics = Graphics.FromImage(img);
            foreach (var item in items)
            {
                var x = item.X;
                var y = item.Y;
                var width = item.Width;
                var height = item.Height;

                var rect = new Rectangle(x, y, width, height);
                var pen = new Pen(Color.Blue,6);
                var point = new Point(x , y);

                graphics.DrawRectangle(pen,rect);
                graphics.DrawString(item.Type, font, brush, point);
            }
            box.Image = img;
        }
    }
}
