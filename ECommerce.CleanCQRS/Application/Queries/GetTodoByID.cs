// Application/Todos/Queries/GetTodoById.cs
using Application.Abstractions;
using Domain;
using Domain.Entity;
using Domain.Exceptions;

namespace Application.Todos.Queries;
public sealed record GetTodoByIdQuery(Guid Id);

public sealed class GetTodoByIdHandler(ITodoReadDb db)
{
    public async Task<TodoItem> Handle(GetTodoByIdQuery q, CancellationToken ct)
        => await db.GetByIdAsync(q.Id, ct) ?? throw new NotFoundException($"Todo {q.Id} not found.");
}
