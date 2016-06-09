using Microsoft.AspNetCore.Mvc.Filters;
using SmallCode.Auth.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SmallCode.Auth.Models;
using Microsoft.AspNetCore.Mvc;

namespace SmallCode.Auth.Filters
{
    public class AuthFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string url = context.HttpContext.Request.Path.Value;
            var cacheService = context.HttpContext.RequestServices.GetService<ICacheService>();
            var db = context.HttpContext.RequestServices.GetService<AuthContext>();

            ///当前用户
            var CurrentUser = db.Users.Where(x => x.UserName == context.HttpContext.User.Identity.Name).SingleOrDefault();

            if (!CacheService.UserPrivileges.ContainsKey(CurrentUser.Id))
            {
                cacheService.SetUserPrivileges(CurrentUser.Id);
            }

            ///当前用户是不是超级管理员
            var count = (from ur in db.UserRoles
                         join r in db.Roles on ur.RoleId equals r.Id
                         where ur.UserId == CurrentUser.Id && r.RoleName == "root"
                         select r).Count();

            if (count <= 0)
            {
                Dictionary<Guid, string> urls = new Dictionary<Guid, string>();
                CacheService.UserPrivileges.TryGetValue(CurrentUser.Id, out urls);
                if (!urls.Values.Contains(url))
                {
                    context.HttpContext.Response.Redirect("/Common/NoAuth");
                }
            }
        }
    }
}
