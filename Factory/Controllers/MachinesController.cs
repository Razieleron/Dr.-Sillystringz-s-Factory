using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;

namespace Factory.Controllers;

public class MachinesController : Controller
{
  private readonly FactoryContext _db;
  public MachinesController(FactoryContext db)
  {
    _db = db;
  }
    public ActionResult Index()
  {
    List<Machine> model = _db.Machines.ToList();
    return View(model);
  }
  public ActionResult Create()
  {
    return View();
  }
  [HttpPost]
  public ActionResult Create(Machine machine)
  {
    _db.Machines.Add(machine);
    _db.SaveChanges();
    return RedirectToAction("Index");
  }

    public ActionResult Details(int id)
  {
    Machine targetMachine = _db.Machines
      .Include(machine => machine.Certifications)
      .ThenInclude(machine => machine.Engineer)
      .FirstOrDefault(machine => machine.MachineId == id);

    return View(targetMachine);
  }

      public ActionResult AddCertifications(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "EngineerName");
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult AddCertifications(Machine machine, int engineerId)
    {
      #nullable enable
      Certification? joinEntity = _db.Certifications.FirstOrDefault(join => (join.EngineerId == engineerId && join.MachineId == machine.MachineId));
      #nullable disable
      if (joinEntity == null && engineerId != 0)
      {
        _db.Certifications.Add(new Certification() { EngineerId = engineerId, MachineId = machine.MachineId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = machine.MachineId });
    }
}