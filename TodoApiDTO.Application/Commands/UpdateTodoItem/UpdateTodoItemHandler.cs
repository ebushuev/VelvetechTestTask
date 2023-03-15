using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TodoApiDTO.Domain;

namespace TodoApiDTO.Application.Commands.UpdateTodoItem
{
    public class UpdateTodoItemHandler : AsyncRequestHandler<UpdateTodoItemCommand>
    {
        private readonly ITodoItemRepository _repository;

        public UpdateTodoItemHandler(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        protected override async Task Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoItem = await _repository.GetById(request.Id);

            if (todoItem == null)
            {
                throw new NotFoundException();
            }

            todoItem.Update(request.Name, request.IsComplete);

            await _repository.Save(todoItem);
        }
    }
}
