using System;

namespace TodoApi.Services.Exceptions
{
    public class NotFoundException : Exception
    {
        private readonly string _objectName;
        private readonly object _id;

        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string objectName, object id)
        {
            _objectName = objectName;
            _id = id;
        }
        public override string Message => _id != null ? $"{_objectName} with id = {_id} not found." : base.Message;
    }
}
