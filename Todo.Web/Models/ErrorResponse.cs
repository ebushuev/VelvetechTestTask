using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace Todo.Web.Models
{
    public class ErrorResponse
    {
        public string ErrorMessage { get; }
        public IDictionary<string, IEnumerable<string>> Errors { get; }

        public ErrorResponse(string errorMessage, IDictionary<string, IEnumerable<string>> errors = null)
        {
            ErrorMessage = errorMessage;
            Errors = errors;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }
}
