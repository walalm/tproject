using Tproject.Domain.Entities;

namespace Tproject.Infrastructure.Services;

public interface IBoardService
{
    Task<Board?> GetByIdAsync(int id);
    Task<Board> CreateAsync(int projectId, string name);
    Task<Column> CreateColumnAsync(int boardId, string name);
}
