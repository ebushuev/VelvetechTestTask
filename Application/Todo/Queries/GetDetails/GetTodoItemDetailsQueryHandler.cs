using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Todo.Queries.GetDetails
{
    public class GetTodoItemDetailsQueryHandler : IRequestHandler<GetTodoItemDetailsQuery, TodoDetailsVm>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetTodoItemDetailsQueryHandler(IDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);
        public async Task<TodoDetailsVm> Handle(GetTodoItemDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.TodoItems.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(HttpStatusCode.NotFound, $"TodoItem {request.Id} not found");

            return _mapper.Map<TodoDetailsVm>(entity);
        }
    }
}


