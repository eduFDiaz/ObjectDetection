using System;
using System.IO;
using System.Linq;
using Alturos.Yolo;
using Alturos.Yolo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AspObjectDetection.Controllers
{
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        YoloWrapper yoloWrapper = new
        YoloWrapper("yolov3/yolov3.cfg", "yolov3/yolov3.weights", "yolov3/coco.names");
        //YoloWrapper("yolov3-tiny/yolov3-tiny.cfg", "yolov3-tiny/yolov3.weights", "yolov3-tiny/coco.names");
        //YoloWrapper("yolov2/yolov2-tiny-voc.cfg", "yolov2/yolov2-tiny-voc.weights", "yolov2/voc.names");

        [HttpPost("[action]")]
        public String Upload([FromForm] IFormFile image)
        {
            try
            {
                using (var target = new MemoryStream())
                {
                    image.CopyTo(target);
                    var _items = this.yoloWrapper.Detect(target.ToArray()).ToList();
                    return _items.ToString();
                }
                
                //string output = JsonConvert.SerializeObject(items);
                //return Content(JsonConvert.SerializeObject(items));

                //string output = JsonConvert.SerializeObject(items);
            }
            catch (Exception e)
            {
                return Content("{0} Exception caught.", e.ToString()).ToString();
            }
        }
    }
}