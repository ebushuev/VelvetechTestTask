
namespace TodoApi.Core.Requests
{
    public class PagedTodoItemRequest
    {
        /// <summary>
        /// Page number
        /// </summary>
        public int PageNumber { get; set; }
        
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }
    }
}
