﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ATM.MVC.Models;

namespace ATM.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    public IActionResult CustomerLogin()
    {
        return View(); 
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AdminLogin()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
