using System.Net;

namespace WebApplication1.Model
{
    public class ApiResponse
    {
        public bool status { get; set; }
        public HttpStatusCode statusCode { get; set; }
        public dynamic Data { get; set; }
        public List<string> Errors { get; set; }
    }
}
