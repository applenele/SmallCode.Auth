using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SmallCode.Auth.Services;
using SmallCode.Auth.Filters;

namespace SmallCode.Auth.Controllers
{
    [AuthFilter]
    public class HomeController : BaseController
    {

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

     

       
    }
}
