using Microsoft.AspNetCore.Mvc;
using Tproject.Infrastructure.Services;
using Tproject.Web.Models;

namespace Tproject.Web.Controllers;

public class ProjectsController : Controller
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    public async Task<IActionResult> Index()
    {
        var projects = await _projectService.GetAllAsync();
        return View(projects);
    }

    public async Task<IActionResult> Details(int id)
    {
        var project = await _projectService.GetByIdAsync(id);
        if (project is null)
        {
            return NotFound();
        }

        return View(project);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateProjectViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var projects = await _projectService.GetAllAsync();
            ViewBag.ShowCreateForm = true;
            return View("Index", projects);
        }

        await _projectService.CreateAsync(model.Name, model.Description);
        return RedirectToAction(nameof(Index));
    }
}
