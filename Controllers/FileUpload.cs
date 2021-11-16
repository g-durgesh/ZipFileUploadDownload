using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ZipFileUploadDownload.Controllers
{
    public class FileUpload : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("FileUpload")]
        public async Task<IActionResult> Index(List<IFormFile> files)
        {


            //string startPath = @"c:\example\start";
            //string zipPath = @"c:\example\result.zip";
            //string extractPath = @"c:\example\extract";

            //System.IO.Compression.ZipFile.CreateFromDirectory(startPath, zipPath);
            //System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);







            long size = files.Sum(f => f.Length);

            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    // full path to file in temp location
                    var filePath = Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.
                    filePaths.Add(filePath);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return Ok(new { count = files.Count, size, filePaths });
        }
    }
}
