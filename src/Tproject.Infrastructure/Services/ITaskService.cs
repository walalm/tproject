using Tproject.Domain.Entities;

namespace Tproject.Infrastructure.Services;

public interface ITaskService
{
    Task<TaskItem> CreateAsync(int columnId, string title, string? description);
    Task MoveTaskAsync(int taskId, int columnId, int newPosition);
}
