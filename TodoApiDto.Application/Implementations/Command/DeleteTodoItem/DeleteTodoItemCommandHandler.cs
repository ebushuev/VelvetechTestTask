using MediatR;
using System.Threading.Tasks;
using System.Threading;
using TodoApiDto.Application.Interfaces;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using Microsoft.Extensions.Logging;
using System;

namespace TodoApiDto.Application.Implementations.Command.DeleteTodoItem
{
    public class DeleteTodoItemCommandHandler: IRequestHandler<DeleteTodoItemCommand, Unit>
    {
        private readonly ITodoApiDtoDbContext _context;
        private readonly ILogger<DeleteTodoItemCommandHandler> _logger;

        public DeleteTodoItemCommandHandler(ITodoApiDtoDbContext context, ILogger<DeleteTodoItemCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var item = _context.Items.Where(x => x.Id == request.Id);
            if(item == null)
            {
                _logger.LogWarning($"Element with this id: {request.Id} does not exist");
                var responseContent = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"There is no item with this {request.Id}")
                };
                throw new HttpResponseException(responseContent);
            }
            try
            {
                _context.Items.RemoveRange(item);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation($"element with id : {request.Id} was removed");
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Error when deleting element with id : {request.Id} : {ex.Message}");
            }
            
            return Unit.Value;
        }
    }
}