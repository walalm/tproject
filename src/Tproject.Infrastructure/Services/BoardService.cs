using Microsoft.EntityFrameworkCore;
using Tproject.Domain.Entities;
using Tproject.Infrastructure.Data;

namespace Tproject.Infrastructure.Services;

public class BoardService : IBoardService
{
    private readonly AppDbContext _context;

    public BoardService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Board?> GetByIdAsync(int id)
    {
        return await _context.Boards
            .Include(b => b.Project)
            .Include(b => b.Columns.OrderBy(c => c.Position))
                .ThenInclude(c => c.Tasks.OrderBy(t => t.Position))
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Board> CreateAsync(int projectId, string name)
    {
        var maxPosition = await _context.Boards
            .Where(b => b.ProjectId == projectId)
            .Select(b => (int?)b.Position)
            .MaxAsync() ?? -1;

        var board = new Board
        {
            ProjectId = projectId,
            Name = name,
            Position = maxPosition + 1
        };

        _context.Boards.Add(board);
        await _context.SaveChangesAsync();
        return board;
    }

    public async Task<Column> CreateColumnAsync(int boardId, string name)
    {
        var maxPosition = await _context.Columns
            .Where(c => c.BoardId == boardId)
            .Select(c => (int?)c.Position)
            .MaxAsync() ?? -1;

        var column = new Column
        {
            BoardId = boardId,
            Name = name,
            Position = maxPosition + 1
        };

        _context.Columns.Add(column);
        await _context.SaveChangesAsync();
        return column;
    }
}
