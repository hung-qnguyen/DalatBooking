using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookingApp.Models;
using BookingApp.Repository.IRepository;

namespace BookingApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Hotel> hotelList = _unitOfWork.Hotel.GetAll(includeProperties:"Category");
        return View(hotelList);
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult About()
    {
        return View();
    }
    public IActionResult Contact()
    {
        return View();
    }
    public IActionResult Hotels()
    {
        return View();
    }
    public IActionResult Details()
    {
        return View();
    }

    public IActionResult Pay()
    {
        return View();
    }
    public IActionResult Thanks()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
