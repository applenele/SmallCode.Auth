using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace SmallCode.Auth.Controllers
{
    public class HomeController : BaseController
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetMenu()
        {
            string menuStr = System.IO.File.ReadAllText("menu.txt");
            return Content(menuStr);
        }
    }
}
