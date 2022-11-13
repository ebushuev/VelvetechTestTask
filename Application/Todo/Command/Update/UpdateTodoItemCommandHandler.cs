using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Todo.Command.Update
{
    public class UpdateTodoItemCommandHandler:IRequestHandler<UpdateTodoItemCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateTodoItemCommandHandler(IDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<Unit> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {

            var item = await _dbContext.TodoItems.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (item == null)
                throw new NotFoundException(HttpStatusCode.NotFound, $"TodoItem {request.Id} not found");

            _mapper.Map(request, item);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
