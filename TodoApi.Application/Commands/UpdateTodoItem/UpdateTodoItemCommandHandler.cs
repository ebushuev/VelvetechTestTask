using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TodoApi.Domain.BusinessRules;
using TodoApi.Infrastructure.DataAccess;

namespace TodoApi.Application.Commands.UpdateTodoItem
{
    internal class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, bool>
    {
        private readonly ITodoDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITodoItemBusinessRules _businessRules;

        public UpdateTodoItemCommandHandler(ITodoDbContext context, IMapper mapper,
            ITodoItemBusinessRules businessRules)
        {
            _context = context;
            _mapper = mapper;
            _businessRules = businessRules;
        }


        public async Task<bool> Handle(UpdateTodoItemCommand request,
            CancellationToken cancellationToken)
        {
            var query = _context.TodoItems.AsQueryable();
            var model = await _businessRules.GetByToDoItemId(query, request.Item.Id, cancellationToken);

            if (model == null) return false;

            _mapper.Map(request.Item, model);

            return (await _context.SaveChangesAsync(cancellationToken)) > 0;
        }
    }
}