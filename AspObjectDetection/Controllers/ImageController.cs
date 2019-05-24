using System;
using System.IO;
using System.Linq;
using Alturos.Yolo;
using Alturos.Yolo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;
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
        [HttpPost("[action]")]
        public ActionResult Upload([FromForm] IFormFile image)
        {
            try
            {
                //var configurationDetector = new ConfigurationDetector();
                //var config = configurationDetector.Detect();
                //var yoloWrapper = new YoloWrapper(config);
                //var items = yoloWrapper.Detect(image.ToString());
                using (var yoloWrapper = new YoloWrapper("yolov2-tiny-voc.cfg", "yolov2-tiny-voc.weights", "voc.names"))
                {
                    var items = yoloWrapper.Detect(image.ToString());
                    //string output = JsonConvert.SerializeObject(items);
                    //return Content(JsonConvert.SerializeObject(items));
                }
                //string output = JsonConvert.SerializeObject(items);
                return Content("not");
            }
            catch (Exception e)
            {
                return Content("{0} Exception caught.", e.ToString());
            }
        }
    }
}