using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;

namespace Factory.Controllers;
public class EngineersController : Controller
{
  private readonly FactoryContext _db;
  public EngineersController(FactoryContext db)
  {
    _db = db;
  }
    public ActionResult Index()
  {
    List<Engineer> model = _db.Engineers.ToList();
    return View(model);
  }
  public ActionResult Create()
  {
    return View();
  }
  [HttpPost]
  public ActionResult Create(Engineer engineer)
  {
    _db.Engineers.Add(engineer);
    _db.SaveChanges();
    return RedirectToAction("Index");
  }

  public ActionResult Details(int id)
  {
    Engineer targetEngineer = _db.Engineers
      .Include(engineer => engineer.Certifications)
      .ThenInclude(engineer => engineer.Machine)
      .FirstOrDefault(engineer => engineer.EngineerId == id);

    return View(targetEngineer);
  }

      public ActionResult AddCertifications(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "MachineName");
      return View(thisEngineer);
    }

    [HttpPost]
    public ActionResult AddCertifications(Engineer engineer, int machineId)
    {
      #nullable enable
      Certification? joinEntity = _db.Certifications.FirstOrDefault(join => (join.MachineId == machineId && join.EngineerId == engineer.EngineerId));
      #nullable disable
      if (joinEntity == null && machineId != 0)
      {
        _db.Certifications.Add(new Certification() { MachineId = machineId, EngineerId = engineer.EngineerId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = engineer.EngineerId });
    }
}