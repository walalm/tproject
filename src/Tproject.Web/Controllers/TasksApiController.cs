using Microsoft.AspNetCore.Mvc;
using Tproject.Infrastructure.Services;
using Tproject.Web.Models;

namespace Tproject.Web.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksApiController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksApiController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost("move")]
    public async Task<IActionResult> Move([FromBody] MoveTaskRequest request)
    {
        if (request.TaskId <= 0 || request.ColumnId <= 0 || request.NewPosition < 0)
        {
            return BadRequest();
        }

        try
        {
            await _taskService.MoveTaskAsync(request.TaskId, request.ColumnId, request.NewPosition);
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
