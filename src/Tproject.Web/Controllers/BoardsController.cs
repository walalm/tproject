using Microsoft.AspNetCore.Mvc;
using Tproject.Infrastructure.Services;
using Tproject.Web.Models;

namespace Tproject.Web.Controllers;

public class BoardsController : Controller
{
    private readonly IBoardService _boardService;

    public BoardsController(IBoardService boardService)
    {
        _boardService = boardService;
    }

    public async Task<IActionResult> Details(int id)
    {
        var board = await _boardService.GetByIdAsync(id);
        if (board is null)
        {
            return NotFound();
        }

        return View(board);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateBoardViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Details", "Projects", new { id = model.ProjectId });
        }

        var board = await _boardService.CreateAsync(model.ProjectId, model.Name);
        return RedirectToAction(nameof(Details), new { id = board.Id });
    }
}
