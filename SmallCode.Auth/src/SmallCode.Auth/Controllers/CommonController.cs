using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SmallCode.Auth.DataModel;
using SmallCode.Auth.Models;
using Microsoft.Extensions.DependencyInjection;
using SmallCode.Auth.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmallCode.Auth.Controllers
{
    public class CommonController : Controller
    {
        public AuthContext db { get { return HttpContext.RequestServices.GetService<AuthContext>(); } }

        public Guid CurrentUserId { set; get; }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult NoAuth()
        {
            return View();
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

        public IActionResult GetMenu()
        {
            List<Menu> menus = new List<Menu>();
            List<Menu> list = new List<Menu>();

            var CurrentUser = db.Users.Where(x => x.UserName == HttpContext.User.Identity.Name).SingleOrDefault();

            if (IsRoot(CurrentUser.Id))
            {
                menus = (from f in db.Functions
                         where f.FatherId == null && f.FunctionCode != "#"
                         && f.FunctionType == FunctionType.View
                         select new Menu
                         {
                             id = f.Id.ToString(),
                             url = f.Url,
                             text = f.FunctionName,
                         }).ToList();

                list.AddRange(menus);
                GetRootMenus(menus, list);
            }
            else
            {
                CurrentUserId = CurrentUser.Id;
                Dictionary<Guid, string> urls = new Dictionary<Guid, string>();

                CacheService.UserPrivileges.TryGetValue(CurrentUser.Id, out urls);

                menus = (from f in db.Functions
                         where f.FatherId == null && f.FunctionCode != "#"
                         && f.FunctionType == FunctionType.View
                         && urls.Keys.Contains(f.Id)
                         select new Menu
                         {
                             id = f.Id.ToString(),
                             url = f.Url,
                             text = f.FunctionName,
                         }).ToList();

                list.AddRange(menus);
                GetMenus(menus, list);
            }


            //string menuStr = System.IO.File.ReadAllText("menu.txt");
            //return Content(menuStr);
            return Json(menus);
        }

        public void GetRootMenus(List<Menu> menus, List<Menu> list)
        {
            List<Menu> tempList = new List<Menu>();
            tempList.AddRange(list);
            if (list == null)
            {
                return;
            }
            if (list.Count == 0)
            {
                return;
            }

            foreach (var menu in tempList)
            {
                var _list = (from f in db.Functions
                             where f.FatherId == new Guid(menu.id)
                              && f.FunctionType == FunctionType.View
                             select new Menu
                             {
                                 id = f.Id.ToString(),
                                 url = f.Url,
                                 text = f.FunctionName,
                                 pid = menu.id,
                             }).ToList();
                if (_list.Count != 0)
                {
                    menus.AddRange(_list);
                    GetRootMenus(menus, _list);
                }
                else
                {
                    continue;
                }
            }
        }


        public void GetMenus(List<Menu> menus, List<Menu> list)
        {
            List<Menu> tempList = new List<Menu>();
            tempList.AddRange(list);
            if (list == null)
            {
                return;
            }
            if (list.Count == 0)
            {
                return;
            }
            Dictionary<Guid, string> urls = new Dictionary<Guid, string>();

            CacheService.UserPrivileges.TryGetValue(CurrentUserId, out urls);

            foreach (var menu in tempList)
            {
                var _list = (from f in db.Functions
                             where f.FatherId == new Guid(menu.id)
                              && f.FunctionType == FunctionType.View
                              && urls.Keys.Contains(f.Id)
                             select new Menu
                             {
                                 id = f.Id.ToString(),
                                 url = f.Url,
                                 text = f.FunctionName,
                                 pid = menu.id,
                             }).ToList();
                if (_list.Count != 0)
                {
                    menus.AddRange(_list);
                    GetMenus(menus, _list);
                }
                else
                {
                    continue;
                }
            }
        }


    }
}
