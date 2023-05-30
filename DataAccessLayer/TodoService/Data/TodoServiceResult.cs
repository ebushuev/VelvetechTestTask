using System.Net;

namespace TodoApi.Services
{
    public class TodoServiceResult<T>
    {
        public T Result { get; set; }

        public HttpStatusCode ResultStatus { get; set; }

        public bool Success { get; set; }
    }
}
