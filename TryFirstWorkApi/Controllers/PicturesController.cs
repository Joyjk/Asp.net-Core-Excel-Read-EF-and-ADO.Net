using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TryFirstWorkApi.Models;

namespace TryFirstWorkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly ILogger<Picture> logger;
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public PicturesController(ILogger<Picture> logger, ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }
        private string uploadFiles(IFormFile objectFile)
        {
            try
            {
                if (objectFile.Length > 0)
                {
                    string path = webHostEnvironment.WebRootPath + "\\uploads\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + objectFile.FileName))
                    {
                        objectFile.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                }
                else
                {
                    return "Not uploaded";
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
            return "uploaded";
        }


        [HttpPost]
        public string Post([FromForm] FileUpload objectFile)
        {
            foreach (var item in objectFile.files)
            {
                string s =   uploadFiles(item);
            }
            return "uploaded";
        }
        [HttpGet("{fileName}")]
        public async Task<IActionResult> Get([FromRoute] string fileName)
        {
            string path = webHostEnvironment.WebRootPath + "\\uploads\\";
            var filePath = path + fileName + ".jpg";
            if(System.IO.File.Exists(filePath))
            {
                byte[] b = System.IO.File.ReadAllBytes(filePath);
                return File(b, "image/jpg");
            }
            return NoContent();
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            List<Picture> pictureList = new List<Picture>();

            string[] filePaths = Directory.GetFiles(Path.Combine(this.webHostEnvironment.WebRootPath, "uploads/"));
            
            List<string> fileNames = new List<string>();

            foreach (var item in filePaths)
            {
                fileNames.Add(Path.GetFileName(item));

            }
            string path = webHostEnvironment.WebRootPath + "\\uploads\\";
            
            foreach (var item in fileNames)
            {
                var filePath = path + item;
                if (System.IO.File.Exists(filePath))
                {
                    byte[] b = System.IO.File.ReadAllBytes(filePath);

                    pictureList.Add(new Picture() { Title = item, Pic = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}" + "/uploads/"+item });

                    //return File(b, "image/jpg");
                }
            }

            var currentUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}"+"/uploads/";

            return Ok(pictureList);
        }

        //public async Task<IActionResult> Create(Picture picture)
        //{
        //    string wwwrootPath = webHostEnvironment.WebRootPath;
        //    string fileName = Path.GetFileNameWithoutExtension(picture.files[0].FileName);
        //    string extension =  Path.GetExtension(picture.files[0].FileName);

        //    Picture pic = new Picture();
        //    pic.Title = fileName;
        //    pic.Pic = Path.

        //}

    }
}
