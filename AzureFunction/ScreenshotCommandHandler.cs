using AzureClassLibrary.Link;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunction
{
    public interface IScreenshotCommandHandler
    {
        Task Handle(string url);
    }

    public class ScreenshotCommandHandler : IScreenshotCommandHandler
    {
        public static RestClient Rclient = new RestClient();

        public async Task Handle(string url)
        {
            Rclient.BaseUrl = new Uri("http://FawaazDa-PC.entelect.local:8795/api/TestSS");

            RestRequest request = new RestRequest(Method.POST);

            request.AddParameter("urlString", url);

            var response = await Rclient.ExecuteAsync(request);
        }
    }
}
