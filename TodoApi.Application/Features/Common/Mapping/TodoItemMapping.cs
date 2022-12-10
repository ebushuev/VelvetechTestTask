using TodoApi.Core;
using TodoApi.Database.Models;

namespace TodoApiDTO.Application.Features.Common.Mapping;

internal static class TodoItemMapping
{
    internal static TodoItem MapToModel(this TodoItemDbo dbo)
    {
        return new TodoItem(
            dbo.Id,
            dbo.Name,
            dbo.IsComplete);
    }
}