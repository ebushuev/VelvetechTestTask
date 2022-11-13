using System;
using System.Collections.Generic;
using System.Text;
using TodoApi.Common.Constants;

namespace TodoApi.Common.Exceptions
{
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException(long id) 
            : base(string.Format(ErrorMessages.DataNotFoundErrorMessage, id))
        {
        }
    }
}
