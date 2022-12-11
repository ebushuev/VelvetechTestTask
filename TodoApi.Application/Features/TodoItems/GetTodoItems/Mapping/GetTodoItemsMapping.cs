using System.Collections.Immutable;
using TodoApi.Database.Models;
using TodoApiDTO.Application.Features.Common.Mapping;

namespace TodoApiDTO.Application.Features.TodoItems.GetTodoItems.Mapping;

internal static class GetTodoItemsMapping
{
    internal static GetTodoItemsResult MapToModel(this TodoItemDbo[] items)
    {
        return new GetTodoItemsResult(items
            .Select(item => item.MapToModel())
            .ToImmutableArray());
    }
}