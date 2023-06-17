using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.DAL.DbContexts;

namespace Todo.BL.UseCases.TodoItems.Commands.UpdateTodoItem
{
    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommandRequest>
    {
        private readonly IMapper _mapper;
        private readonly TodoContext _context;

        public UpdateTodoItemCommandHandler(IMapper mapper, TodoContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTodoItemCommandRequest request, CancellationToken cancellationToken)
        {
            var todoItem = await _context.TodoItems.SingleOrDefaultAsync(x => x.Id == request.TodoItemDto.Id, cancellationToken);
            if (todoItem == null)
                throw new Exception("Todo Item Not Found");

            _mapper.Map(request.TodoItemDto, todoItem);

            try
            {
                _context.TodoItems.Update(todoItem);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Please, try again");
            }


            return Unit.Value;
        }
    }
}