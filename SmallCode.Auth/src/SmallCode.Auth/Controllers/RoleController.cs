using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.Auth.DataModel;
using SmallCode.Auth.Models;
using SmallCode.Auth.Extensions;

namespace SmallCode.Auth.Controllers
{
    public class RoleController : BaseController
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResult RoleList(PageRequestModel model)
        {
            IQueryable<Role> query = db.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(model.key))
            {
                query = query.Where(x => x.RoleName.Contains(model.key));
            }
            if (!string.IsNullOrEmpty(model.sortOrder))
            {
                bool flag = model.sortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase);
                query = query.OrderBy(model.sortField, flag);
            }
            int index = model.pageIndex * model.pageSize;

            List<Role> roles = query.Skip(index).Take(model.pageSize).ToList();
            return Json(roles);
        }


        #region 角色修改
        /// <summary>
        /// 角色修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(Guid? id)
        {
            Role role = new Models.Role();
            bool IsInsert = true;
            if (id.HasValue)
            {
                role = db.Roles.First(x => x.Id == id);
                IsInsert = false;
            }
            ViewBag.IsInsert = IsInsert;
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Role role)
        {
            role.CreatedBy = CurrentUser.Id;
            role.CreatedDate = DateTime.Now;
            role.IsDelete = false;
            db.Roles.Add(role);
            db.SaveChanges();
            return View();
        }
        #endregion


        public IActionResult Remove(string ids)
        {
            if (string.IsNullOrEmpty(ids)) return Content("error");
            List<Guid> idList = new List<Guid>();
            ids.Split(',').ToList().ForEach(x => idList.Add(new Guid(x)));
            List<Role> roles = db.Roles.Where(x => idList.Contains(x.Id)).ToList();
            db.Roles.RemoveRange(roles);
            db.SaveChanges();
            return Content("ok");
        }

    }
}
