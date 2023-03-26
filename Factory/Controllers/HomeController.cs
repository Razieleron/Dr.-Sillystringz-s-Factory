using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;

namespace Factory.Controllers;

public class HomeController : Controller
{
  private readonly FactoryContext _db;
  public HomeController(FactoryContext db)
      {
        _db = db;
      }

  [Route("/")]
  public ActionResult Index() 
  {
    ViewBag.engineers = _db.Engineers.ToList();
    ViewBag.machines = _db.Machines.ToList();
    return View();
  }

}
