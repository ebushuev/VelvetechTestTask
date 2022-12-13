using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.Common.Contract
{
    public enum  ResponseCode
    {
        Success = 2000,
        DataNotFound = 2001,
        InternalServerError = 2002,
        ValidationError = 2003,
        DBUpdateError = 2004,
        BadRequest = 2005,
        DataAlreadyExist = 2006
    }
}
