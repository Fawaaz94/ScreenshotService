using Freezer.Core;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web1.Code;

namespace Web1.Controllers
{
    [Route("api/[controller]")]
    public class TestSSController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }


        #region DLL Screenshot -- Works
        //[HttpPost]
        //public IActionResult Index(string urlString)
        //{

        //    var _Obj = new WebsitesScreenshot.WebsitesScreenshot();

        //    _Obj.Force = true;
        //    WebsitesScreenshot.WebsitesScreenshot.Result _Result = _Obj.CaptureWebpage(urlString);
            
        //    var splitHostName = urlString.Split('.');

        //    string siteName = splitHostName[1];

        //    if (_Result == WebsitesScreenshot.WebsitesScreenshot.Result.Captured)
        //    {
        //        _Obj.ImageFormat = WebsitesScreenshot.WebsitesScreenshot.ImageFormats.JPG;

                
        //        _Obj.SaveImage($"D:\\{siteName}.jpg");

        //        //blobstorenet.sendByteArrayToBlob(siteName, _Result);
        //    }

        //    _Obj.Dispose();

        //    return View();
        //}
        #endregion

        #region Nuget Freezer Screenshot
        [HttpPost]
        public IActionResult Index(string urlString)
        {

            byte[] imageAsBytes = null;

            var splitHostName = urlString.Split('.');
            string siteName = splitHostName[1];


            var screenshotJob = ScreenshotJobBuilder.Create(urlString)
                .SetBrowserSize(1366, 768)
                .SetCaptureZone(CaptureZone.VisibleScreen) // Set what should be captured
                .SetTrigger(new WindowLoadTrigger())
                .Freeze(); // Set when the picture is taken

            imageAsBytes = screenshotJob.AsBytes();


            blobstorenet.sendByteArrayToBlob(siteName, imageAsBytes);

            //System.IO.File.WriteAllBytes($"D:\\{siteName}.jpg", imageAsBytes);

            return View();
        }
        #endregion




    }
}