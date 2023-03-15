using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TodoApiDTO.Infrastructure.EfCore;

namespace TodoApiDTO.Application.Queries.GetTodoItems
{
    public class GetTodoItemsHandler : IRequestHandler<GetTodoItemsQuery, IEnumerable<TodoItemViewModel>>
    {
        private readonly TodoApiDTOContext _db;

        public GetTodoItemsHandler(TodoApiDTOContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<TodoItemViewModel>> Handle(GetTodoItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _db.TodoItems.ToArrayAsync();

            return items.Select(x => TodoItemViewModel.MapFrom(x));
        }
    }
}
