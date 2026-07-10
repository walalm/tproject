using Microsoft.AspNetCore.Mvc;
using Tproject.Infrastructure.Services;
using Tproject.Web.Models;

namespace Tproject.Web.Controllers;

public class TasksController : Controller
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateTaskViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Details", "Boards", new { id = model.BoardId });
        }

        await _taskService.CreateAsync(model.ColumnId, model.Title, model.Description);
        return RedirectToAction("Details", "Boards", new { id = model.BoardId });
    }
}
