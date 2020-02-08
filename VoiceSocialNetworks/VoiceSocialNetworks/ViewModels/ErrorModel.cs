using System.Net;

namespace VoiceSocialNetworks.ViewModels
{
    public class ErrorModel
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
