using Microsoft.EntityFrameworkCore;
using Tproject.Domain.Entities;
using Tproject.Infrastructure.Data;

namespace Tproject.Infrastructure.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _context;

    public TaskService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TaskItem> CreateAsync(int columnId, string title, string? description)
    {
        var maxPosition = await _context.Tasks
            .Where(t => t.ColumnId == columnId)
            .Select(t => (int?)t.Position)
            .MaxAsync() ?? -1;

        var task = new TaskItem
        {
            ColumnId = columnId,
            Title = title,
            Description = description,
            Position = maxPosition + 1
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task MoveTaskAsync(int taskId, int columnId, int newPosition)
    {
        var task = await _context.Tasks.FindAsync(taskId)
            ?? throw new KeyNotFoundException($"Task {taskId} not found.");

        var oldColumnId = task.ColumnId;
        var oldPosition = task.Position;

        if (oldColumnId == columnId)
        {
            await ReorderWithinColumnAsync(columnId, taskId, oldPosition, newPosition);
        }
        else
        {
            await RemoveFromColumnAsync(oldColumnId, oldPosition);
            await InsertIntoColumnAsync(columnId, taskId, newPosition);
        }

        task.ColumnId = columnId;
        task.Position = newPosition;
        await _context.SaveChangesAsync();
    }

    private async Task ReorderWithinColumnAsync(int columnId, int taskId, int oldPosition, int newPosition)
    {
        if (oldPosition == newPosition) return;

        var tasks = await _context.Tasks
            .Where(t => t.ColumnId == columnId && t.Id != taskId)
            .OrderBy(t => t.Position)
            .ToListAsync();

        if (newPosition < oldPosition)
        {
            foreach (var t in tasks.Where(t => t.Position >= newPosition && t.Position < oldPosition))
            {
                t.Position++;
            }
        }
        else
        {
            foreach (var t in tasks.Where(t => t.Position > oldPosition && t.Position <= newPosition))
            {
                t.Position--;
            }
        }
    }

    private async Task RemoveFromColumnAsync(int columnId, int removedPosition)
    {
        var tasks = await _context.Tasks
            .Where(t => t.ColumnId == columnId && t.Position > removedPosition)
            .ToListAsync();

        foreach (var t in tasks)
        {
            t.Position--;
        }
    }

    private async Task InsertIntoColumnAsync(int columnId, int taskId, int newPosition)
    {
        var tasks = await _context.Tasks
            .Where(t => t.ColumnId == columnId && t.Id != taskId && t.Position >= newPosition)
            .ToListAsync();

        foreach (var t in tasks)
        {
            t.Position++;
        }
    }
}
