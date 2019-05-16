using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspObjectDetection.Controllers
{
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {

        //[HttpPost]
        [HttpPost("[action]")]
        public ActionResult Upload([FromForm] IFormFile image)
        {
            Console.WriteLine("hello from controller Image");
            if (image == null || image.Length == 0)
                throw new Exception("File is empty!");

            return Content("hello world!"+image.FileName);
        }
    }
}