using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.Auth.Models;
using Microsoft.Extensions.DependencyInjection;
using SmallCode.Auth.Helper;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;

namespace SmallCode.Auth.Controllers
{
    public class AccountController : BaseController
    {
        // GET: /<controller>/
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string UserName, string Password)
        {
            User user = db.Users.Where(x => x.UserName == UserName && x.Password == Password.ToMD5Hash()).FirstOrDefault();
            if (user != null)
            {

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, UserName));

                ///登录写入cookie 设置过期时间 这个时间是滑动时间　不是绝对的时间
                await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                          new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)),
                          new AuthenticationProperties { ExpiresUtc = DateTime.UtcNow.AddMinutes(30) });

                return Redirect("/Home/Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Account/Login");
        }

    }
}
