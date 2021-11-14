using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [Route("file")]
    public class FileControler : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [ResponseCache(Duration = 1200, VaryByQueryKeys = new[] { "fileName" })]
        public ActionResult GetFile([FromQuery] string fileName)
        {
            string rootPath = Directory.GetCurrentDirectory();
            string filePath = $"{rootPath}/PrivateFiles/{fileName}";
            bool fileExists = System.IO.File.Exists(filePath);

            if (!fileExists)
            {
                return NotFound();
            }
            var contentProvider = new FileExtensionContentTypeProvider();
            string contentType = string.Empty;
            contentProvider.TryGetContentType(filePath, out contentType);
            byte[] fileContents = System.IO.File.ReadAllBytes(filePath);
            return File(fileContents, contentType, fileName);
        }

        [HttpPost]
        public ActionResult Upload([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest();

            string rootPath = Directory.GetCurrentDirectory();
            string fileName = file.FileName;
            string fullPath = $"{rootPath}/PrivateFiles/{fileName}";
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return Ok();
        }
    }
}
