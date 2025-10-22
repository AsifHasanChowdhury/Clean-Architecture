// Infrastructure/Todos/TodoWriteDb.cs
using Application.Abstractions;
using Dapper;
using Domain;
using Domain.Entity;
using Domain.Exceptions;
using Infrastructure.Db;
using System.Data;

namespace Infrastructure.Todos;

public sealed class TodoWriteDb(IConnectionFactory factory) : ITodoWriteDb
{
    public async Task CreateAsync(TodoItem item, CancellationToken ct)
    {
        using var conn = factory.Create();
        var cmd = new CommandDefinition(
            @"INSERT INTO dbo.Todos (Id, Title, IsDone, CreatedUtc)
              VALUES (@Id, @Title, @IsDone, SYSUTCDATETIME());",
            new { item.Id, item.Title, IsDone = item.IsDone }, cancellationToken: ct);
        await conn.ExecuteAsync(cmd);
    }

    public async Task UpdateAsync(TodoItem item, CancellationToken ct)
    {
        using var conn = factory.Create();
        var cmd = new CommandDefinition(
            @"UPDATE dbo.Todos
              SET Title=@Title, IsDone=@IsDone, UpdatedUtc=SYSUTCDATETIME()
              WHERE Id=@Id;",
            new { item.Id, item.Title, item.IsDone }, cancellationToken: ct);
        var rows = await conn.ExecuteAsync(cmd);
        if (rows == 0) throw new NotFoundException($"Todo {item.Id} not found.");
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        using var conn = factory.Create();
        var cmd = new CommandDefinition(
            @"DELETE FROM dbo.Todos WHERE Id=@Id;", new { Id = id }, cancellationToken: ct);
        var rows = await conn.ExecuteAsync(cmd);
        if (rows == 0) throw new NotFoundException($"Todo {id} not found.");
    }
}
