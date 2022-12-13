using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.Common.Contract
{
    public class ResponseMessage
    {
        public static readonly Dictionary<ResponseCode, string> ResponseMessages
            = new Dictionary<ResponseCode, string>
        {
            { ResponseCode.Success, "Success" },
            { ResponseCode.DataNotFound, "Data could not be found" },
            { ResponseCode.InternalServerError, "Internal server error occured" }
        };
    }
}
