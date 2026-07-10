using Tproject.Domain.Entities;

namespace Tproject.Infrastructure.Services;

public interface IProjectService
{
    Task<IReadOnlyList<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(int id);
    Task<Project> CreateAsync(string name, string? description);
}
