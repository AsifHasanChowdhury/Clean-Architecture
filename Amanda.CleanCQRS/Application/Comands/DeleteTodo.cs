// Application/Todos/Commands/DeleteTodo.cs
using Application.Abstractions;
using Domain;
using Domain.Exceptions;

namespace Application.Todos.Commands;
public sealed record DeleteTodoCommand(Guid Id);

public sealed class DeleteTodoHandler(ITodoWriteDb db, ITodoReadDb rdb)
{
    public async Task Handle(DeleteTodoCommand cmd, CancellationToken ct)
    {
        _ = await rdb.GetByIdAsync(cmd.Id, ct) ?? throw new NotFoundException($"Todo {cmd.Id} not found.");
        await db.DeleteAsync(cmd.Id, ct);
    }
}
