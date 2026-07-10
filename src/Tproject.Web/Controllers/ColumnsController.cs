using Microsoft.AspNetCore.Mvc;
using Tproject.Infrastructure.Services;
using Tproject.Web.Models;

namespace Tproject.Web.Controllers;

public class ColumnsController : Controller
{
    private readonly IBoardService _boardService;

    public ColumnsController(IBoardService boardService)
    {
        _boardService = boardService;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateColumnViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Details", "Boards", new { id = model.BoardId });
        }

        await _boardService.CreateColumnAsync(model.BoardId, model.Name);
        return RedirectToAction("Details", "Boards", new { id = model.BoardId });
    }
}
