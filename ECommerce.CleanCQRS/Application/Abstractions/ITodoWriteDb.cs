// Application/Abstractions/ITodoWriteDb.cs
using Domain;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Application.Abstractions;
public interface ITodoWriteDb
{
    Task CreateAsync(TodoItem item, CancellationToken ct);
    Task UpdateAsync(TodoItem item, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}

