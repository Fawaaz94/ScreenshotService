using System.Threading.Tasks;
using AzureClassLibrary.QueueConnection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using AzureClassLibrary;
using ScreenshotService.BlobStore;
using Newtonsoft.Json;

namespace ScreenshotService.Controllers
{
    //[ApiController]
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly IQueueCommunicator _queueCommunicator;

        public TestController(IQueueCommunicator queueCommunicator)
        {
            _queueCommunicator = queueCommunicator;

        }

        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// A controller that will allow you to send data to the Azure Storage Queue
        /// </summary>
        /// <param name="uploadlinks"> Theses are the links for the u</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Index(string uploadlinks)
        {
            var splitLinks = uploadlinks.Split(',');

            foreach (string item in splitLinks)
            {
                await _queueCommunicator.SendAsync(item);
            }


            return View();
        }

        [Route("RetrieveResults")]
        public JsonResult RetrieveResults()
        {
            List<BlobEntity> list = blobstore.GetBlobList();

            //var listSeriazlized = JsonConvert.SerializeObject(list);

            return Json(list);
        }

        [Route("SaveResults")]
        public IActionResult SaveResults(string screenshot)
        {
            try
            {
                blobstore.SaveBlobToPC(screenshot);
            }
            catch (Exception ex)
            {
                throw;
            }
            
            return Ok();
        }

    }
}