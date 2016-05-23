using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SmallCode.Auth.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;

namespace SmallCode.Auth.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        //在RC2中mvc 获取容器里面的东西有所变动
        //之前在Resolver 
        ///https://github.com/aspnet/Mvc/issues/4296
        public AuthContext db { get { return HttpContext.RequestServices.GetService<AuthContext>(); } }

        public User CurrentUser { set; get; }

        public Dictionary<Guid, List<string>> UserPrivileges = new Dictionary<Guid, List<string>>();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                CurrentUser = db.Users.Where(x => x.UserName == HttpContext.User.Identity.Name).SingleOrDefault();
                ViewBag.CurrentUser = CurrentUser;

                if (!UserPrivileges.ContainsKey(CurrentUser.Id))
                {
                    var data = from f in db.Functions
                               join rf in db.RoleFunctions on f.Id equals rf.FunctionId
                               join r in db.Roles on rf.RoleId equals r.Id
                               join ur in db.UserRoles on r.Id equals ur.RoleId
                               join u in db.Users on ur.UserId equals u.Id
                               where u.Id == CurrentUser.Id
                               select new
                               {
                                   f.Url
                               };
                    List<string> urls = new List<string>();
                    foreach (var item in data)
                    {
                        urls.Add(item.Url);
                    }
                    UserPrivileges.Add(CurrentUser.Id, urls);
                }
            }
            ViewBag.CurrentUser = CurrentUser;
            base.OnActionExecuting(context);
        }
    }
}
