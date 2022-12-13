using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.Common.Contract
{
    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; }

        public ApiResponse(T data) : base()
        {
            Data = data;
        }
    }

    public class ApiResponse : BaseMessage
    {
        public ResponseCode Code { get; set; }

        public string Message { get; set; }

        public DateTime TimeStamp { get; set; } = DateTime.Now;


        public ApiResponse() : base()
        {
            Code = ResponseCode.Success;
            Message = "Success";
        }
    }
}
