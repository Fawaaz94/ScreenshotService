using AzureClassLibrary.Infrastructure;
using AzureClassLibrary.Links;

namespace AzureClassLibrary.Link
{
    public class SendScreenshotCommand : BaseMessageQueue
    {
        public string links { get; set; }

        public SendScreenshotCommand() : base(RouteNames.LinksBox)
        {

        }
    }
}
