using MediatR;
using System.Threading.Tasks;
using System.Threading;
using TodoApiDto.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using TodoApiDto.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace TodoApiDto.Application.Implementations.Queries.GetToItem
{
    public class GetToDoItemQueryHandler: IRequestHandler<GetToDoItemQuery, TodoItem>
    {
        private readonly ITodoApiDtoDbContext _context;
        private readonly ILogger<GetToDoItemQueryHandler> _logger;
        public GetToDoItemQueryHandler(ITodoApiDtoDbContext context, ILogger<GetToDoItemQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TodoItem> Handle(GetToDoItemQuery request, CancellationToken cancellationToken)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (item == null)
            {
                _logger.LogWarning($"Element with this id: {request.Id} does not exist");

                var responseContent = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"There is no item with this {request.Id}")
                };
                throw new HttpResponseException(responseContent);
            }
            return item;
        }
    }
}
