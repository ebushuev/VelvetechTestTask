using System.ComponentModel;

namespace TodoApiDTO.ApiConstans
{
    public enum ApiResponseStatus
    {
        [Description("Success")]
        Success,
        [Description("Item does not exist")]
        ItemDoesntExist,
    }
}
