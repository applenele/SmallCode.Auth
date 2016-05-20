using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.Auth.DataModel;
using SmallCode.Auth.Models;
using SmallCode.Auth.Helper;
using SmallCode.Auth.Extensions;

namespace SmallCode.Auth.Controllers
{
    public class UserController : BaseController
    {
        // GET: /<controller>/
        public IActionResult Index(PageRequestModel model)
        {
            return View();
        }


        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResult UserList(PageRequestModel model)
        {
            IQueryable<User> query = db.Users.AsQueryable();
            if (!string.IsNullOrEmpty(model.key))
            {
                query = query.Where(x => x.UserName.Contains(model.key));
            }
            if (!string.IsNullOrEmpty(model.sortOrder))
            {
                bool flag = model.sortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase);
                query = query.OrderBy(model.sortField, flag);
            }
            int index = model.pageIndex * model.pageSize;
            List<User> users = query.Skip(index).Take(model.pageSize).ToList();
            return Json(users);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IActionResult Remove(string ids)
        {
            if (string.IsNullOrEmpty(ids)) return Content("error");
            List<Guid> idList = new List<Guid>();
            ids.Split(',').ToList().ForEach(x => idList.Add(new Guid(x)));
            List<User> users = db.Users.Where(x => idList.Contains(x.Id)).ToList();
            db.Users.RemoveRange(users);
            db.SaveChanges();
            return Content("ok");
        }

        [HttpGet]
        public IActionResult Edit(Guid? id)
        {
            User user = new Models.User();
            bool IsInsert = true;
            if (id.HasValue)
            {
                user = db.Users.First(x => x.Id == id);
                IsInsert = false;
            }
            ViewBag.IsInsert = IsInsert;
            return View();
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            user.Password = "123456".ToMD5Hash();
            user.CreatedBy = CurrentUser.Id;
            user.CreatedDate = DateTime.Now;
            user.IsDelete = false;
            db.Users.Add(user);
            db.SaveChanges();
            return View();
        }


        public IActionResult GetDepartments()
        {
            var departments = db.Departments.ToList();
            return Json(departments);
        }


    }
}
