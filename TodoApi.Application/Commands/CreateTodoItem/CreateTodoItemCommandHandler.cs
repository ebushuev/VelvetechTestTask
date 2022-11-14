using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApi.Domain.BusinessRules;
using TodoApi.Domain.Models;
using TodoApi.Infrastructure.DataAccess;

namespace TodoApi.Application.Commands.CreateTodoItem
{
    internal class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, long>
    {
        private readonly ITodoDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITodoItemBusinessRules _businessRules;

        public CreateTodoItemCommandHandler(ITodoDbContext context, IMapper mapper, ITodoItemBusinessRules businessRules)
        {
            _context = context;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<long> Handle(CreateTodoItemCommand request,
            CancellationToken cancellationToken)
        {
            var item = _mapper.Map<TodoItem>(request.Item);

            await _context.TodoItems.AddAsync(item, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            if (!await _businessRules.TodoItemExists(_context.TodoItems, item.Id, cancellationToken))
                throw new DbUpdateConcurrencyException("Insertion failed");

            return item.Id;
        }
    }
}