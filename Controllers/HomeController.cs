using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZipFileUploadDownload.Models;

namespace ZipFileUploadDownload.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHostingEnvironment _hostingEnvironment;
        public HomeController(ILogger<HomeController> logger, IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public FileResult GenerateAndDownloadZip()
        {
            var webRoot = _hostingEnvironment.WebRootPath;
            var fileName = "Output.zip";
            var tempOutput = webRoot + "/Assets/" + fileName;
            using (ZipOutputStream zipOutput = new ZipOutputStream(System.IO.File.Create(tempOutput)))
            {
                zipOutput.SetLevel(9);
                byte[] buffer = new byte[4096];
                var pdfList = new List<string>();
                pdfList.Add(webRoot + @"/Assets/Cerificate of Incorporation (3) (1).PDF");
                pdfList.Add(webRoot + @"/Assets/Cerificate of Incorporation(1)1.PDF");
                pdfList.Add(webRoot + @"/Assets/Certificate of Incorporation (10).PDF");
                pdfList.Add(webRoot + @"/Assets/Form ADT-1-01042021_signed.pdf");
                pdfList.Add(webRoot + @"/Assets/Form ADT-1-05122020_signed.pdf");
                pdfList.Add(webRoot + @"/Assets/Form ADT-1-06022018_signed.pdf");
                pdfList.Add(webRoot + @"/Assets/Form AOC-4-01122018_signed.pdf");
                pdfList.Add(webRoot + @"/Assets/Form AOC-4-03012019_signed.pdf");
                pdfList.Add(webRoot + @"/Assets/Form AOC-4-04022020_signed.pdf");
                pdfList.Add(webRoot + @"/Assets/Form DIR-12-01022019_signed(1).pdf");
                pdfList.Add(webRoot + @"/Assets/Form DIR-12-01042020_signed.pdf");
                pdfList.Add(webRoot + @"/Assets/Form DIR-12-01062020_signed.pdf");
                pdfList.Add(webRoot + @"/Assets/Form MGT-7-01012020_signed (2).pdf");
                pdfList.Add(webRoot + @"/Assets/Form MGT-7-01102018_signed.pdf");
                pdfList.Add(webRoot + @"/Assets/Form MGT-7-02012020_signed (2) (1).pdf");
                pdfList.Add(webRoot + @"/Assets/Form PAS-3-01012019_signed.pdf");
                pdfList.Add(webRoot + @"/Assets/Form PAS-3-01082019_signed.pdf");
                pdfList.Add(webRoot + @"/Assets/Form PAS-3-02032019_signed.pdf");
                pdfList.Add(webRoot + @"/Assets/Form PAS-6-04112020_signed.pdf");
                pdfList.Add(webRoot + @"/Assets/Form PAS-6-12092020_signed.pdf");
                pdfList.Add(webRoot + @"/Assets/Form PAS-6-11092020_signed.pdf");
                pdfList.Add(webRoot + @"/Assets/Fresh Certificate of Incorporation Consequent upon  Change of Name-080311 (1).PDF");
                pdfList.Add(webRoot + @"/Assets/Fresh Certificate of Incorporation Consequent upon Change of Name on Conversion to Private Limited Company-130310 (2).PDF");
                pdfList.Add(webRoot + @"/Assets/Fresh Certificate of Incorporation Consequent upon Change of Name on Conversion to Private Limited Company-130310.PDF");

                for (int count = 0; count < pdfList.Count; count++)
                {
                    ZipEntry entry = new ZipEntry(Path.GetFileName(pdfList[count]));
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    zipOutput.PutNextEntry(entry);
                    using (FileStream fileStream = System.IO.File.OpenRead(pdfList[count]))
                    {
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fileStream.Read(buffer, 0, buffer.Length);
                            zipOutput.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);

                    }
                }
                zipOutput.Finish();
                zipOutput.Flush();
                zipOutput.Close();
            }
            byte[] finalResult = System.IO.File.ReadAllBytes(tempOutput);
            if (System.IO.File.Exists(tempOutput))
            {
                System.IO.File.Delete(tempOutput);
            }
            if(finalResult == null || !finalResult.Any())
            {
                throw new Exception(String.Format("Nothing Found"));
            }
            return File(finalResult, "application/zip", fileName);
        }
    }
}
