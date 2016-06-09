using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SmallCode.Auth.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using SmallCode.Auth.Filters;
using SmallCode.Auth.DataModel;
using SmallCode.Auth.Services;

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




        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                CurrentUser = db.Users.Where(x => x.UserName == HttpContext.User.Identity.Name).SingleOrDefault();
                ViewBag.CurrentUser = CurrentUser;

                //if (!CacheService.UserPrivileges.ContainsKey(CurrentUser.Id))
                //{
                //    cacheService.SetUserPrivileges(CurrentUser.Id);
                //}


                //if (!IsRoot(CurrentUser.Id))
                //{
                //    string url = HttpContext.Request.Path.Value;
                //    Dictionary<Guid, string> urls = new Dictionary<Guid, string>();

                //    CacheService.UserPrivileges.TryGetValue(CurrentUser.Id, out urls);
                //    if (!urls.Values.Contains(url))
                //    {
                //        HttpContext.Response.Redirect("/Common/NoAuth");
                //    }
                //}
            }
            ViewBag.CurrentUser = CurrentUser;
            base.OnActionExecuting(context);
        }

        /// <summary>
        /// 判断是不是root用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool IsRoot(Guid Id)
        {
            var count = (from ur in db.UserRoles
                         join r in db.Roles on ur.RoleId equals r.Id
                         where ur.UserId == Id && r.RoleName == "root"
                         select r).Count();
            return count > 0;
        }
    }
}
