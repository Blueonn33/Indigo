using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Indigo.Models;

namespace Indigo.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Author()
    {
        return View();
    }

    public IActionResult Publisher()
    {
        return View();
    }

    public IActionResult Reviewer()
    {
        return View();
    }
}
