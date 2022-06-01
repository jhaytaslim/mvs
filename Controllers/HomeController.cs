using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mvccore.Models;
using Newtonsoft.Json;

namespace mvccore.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private Database _database;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _database = new Database();
    }

    public IActionResult Index()
    {
        var timesheet =
                (from sheet in _database.timesheet
                 join emp in _database.users on sheet.username equals emp.username
                 // where s.Entity_ID == getEntity
                 select new TimesheetViewModel { sheet = sheet, user = emp }).ToList();
        //var timesheet = _database.timesheet.ToList();
        return View(timesheet);
    }

    public IActionResult ClockIn(ClockViewModel model)
    {
        var username = model?.username ?? "farge";

        //Console.WriteLine($"clockin........{JsonConvert.SerializeObject(_database.timesheet)}");
        _database.timesheet.Add(new Clocking { username = username, start = DateTime.Now });

        var timesheet =
                (from sheet in _database.timesheet
                 join emp in _database.users on sheet.username equals emp.username
                 where sheet.username == username
                 select new TimesheetViewModel { sheet = sheet, user = emp }).ToList();

        Console.WriteLine($"clockin fin........{JsonConvert.SerializeObject(timesheet)}");
        //var timesheet = _database.timesheet.ToList();
        return PartialView("_PartialTable", timesheet);
        // return Json(timesheet);
    }

    public IActionResult ClockOut(ClockViewModel model)
    {
        var username = model?.username ?? "farge";

        //Console.WriteLine($"clockin........{JsonConvert.SerializeObject(_database.timesheet)}");
        Console.WriteLine($"clockout........{username}");
        Console.WriteLine($"_database.timesheet fin........{JsonConvert.SerializeObject(_database.timesheet)}");
        var currentClock = _database.timesheet.FirstOrDefault<Clocking>(item => item.username == username && item.end == null);

 Console.WriteLine($"currentClock fin........{JsonConvert.SerializeObject(currentClock)}");
        currentClock.end = DateTime.Now;

        var timesheet =
                (from sheet in _database.timesheet
                 join emp in _database.users on sheet.username equals emp.username
                 where sheet.username == username
                 select new TimesheetViewModel { sheet = sheet, user = emp }).ToList();

        Console.WriteLine($"clockin fin........{JsonConvert.SerializeObject(timesheet)}");
        //var timesheet = _database.timesheet.ToList();
        return PartialView("_PartialTable", timesheet);
        // return Json(timesheet);
    }

    // public IActionResult ClockOut(string username = "farge")
    // {
    //     Console.WriteLine($"clockout........{username}");
    //     var currentClock = _database.timesheet.FirstOrDefault<Clocking>(item=> item.username == username && item.end == null);

    //     currentClock.end = DateTime.Now;

    //     var timesheet =
    //             (from sheet in _database.timesheet
    //             join emp in _database.users on sheet.username equals emp.username
    //              where sheet.username == username
    //             select new TimesheetViewModel{sheet = sheet,user=emp}).ToList();
    //     //var timesheet = _database.timesheet.ToList();
    //     return View(timesheet);
    // }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
