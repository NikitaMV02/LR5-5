using Microsoft.AspNetCore.Http;

namespace ApiClientExample
{
    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public int HttpStatusCode { get; set; } 
        public T Data { get; set; }
    }
}
