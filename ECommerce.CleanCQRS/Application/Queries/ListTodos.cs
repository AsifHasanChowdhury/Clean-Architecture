// Application/Todos/Queries/ListTodos.cs
using Application.Abstractions;
using Domain;
using Domain.Entity;

namespace Application.Todos.Queries;
public sealed record ListTodosQuery(int Page = 1, int PageSize = 20);

public sealed class ListTodosHandler(ITodoReadDb db)
{
    public Task<IReadOnlyList<TodoItem>> Handle(ListTodosQuery q, CancellationToken ct)
        => db.ListAsync(q.Page <= 0 ? 1 : q.Page, q.PageSize <= 0 ? 20 : q.PageSize, ct);
}
