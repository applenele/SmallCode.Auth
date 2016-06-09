using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.Auth.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmallCode.Auth.Controllers
{
    public class TestController : Controller
    {

        public AuthContext context;

        public TestController(AuthContext _context)
        {
            context = _context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            context.Add(new Function {FunctionCode="ii",FunctionName="test",Url="iii",FunctionType=FunctionType.View });
            context.SaveChanges();
            return View();
        }
    }
}
