using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;

namespace Factory.Controllers;

public class HomeController : Controller
{

  [Route("/")]
  public ActionResult Index() 
  {
    return View();
  }

}
