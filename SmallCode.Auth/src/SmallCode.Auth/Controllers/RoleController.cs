using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.Auth.DataModel;
using SmallCode.Auth.Models;
using SmallCode.Auth.Extensions;
using SmallCode.Auth.Services;
using SmallCode.Auth.Filters;

namespace SmallCode.Auth.Controllers
{
    [Auth]
    public class RoleController : BaseController
    {

        [Inject]
        public ICacheService cacheService { set; get; }

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
            GridResponseData<Role> response = new GridResponseData<Role>();
            response.data = roles;
            response.total = query.Count();
            return Json(response);
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

        [HttpGet]
        public IActionResult Auth(Guid id)
        {
            ViewBag.RoleId = id;
            List<Function> functions = new List<Function>();
            functions = db.Functions.ToList();
            ViewBag.Functions = functions;
            List<Guid> oldRoleFunctionIdList = new List<Guid>();
            oldRoleFunctionIdList = db.RoleFunctions.Where(x => x.RoleId == id).Select(x => x.FunctionId).ToList();
            ViewBag.FunctionIds = oldRoleFunctionIdList;
            return View();
        }

        [HttpPost]
        public IActionResult Auth(string functionIds, Guid id)
        {
            string[] functionIdArr = functionIds.Substring(1, functionIds.Length - 1).Split(',');
            List<RoleFunction> roleFunctionList = new List<RoleFunction>();
            foreach (var item in functionIdArr)
            {
                roleFunctionList.Add(new RoleFunction
                {
                    CreatedBy = CurrentUser.Id,
                    CreatedDate = DateTime.Now,
                    RoleId = id,
                    FunctionId = new Guid(item),
                    Id = Guid.NewGuid()
                });
            }

            List<RoleFunction> oldRoleFunctionList = new List<RoleFunction>();
            oldRoleFunctionList = db.RoleFunctions.Where(x => x.RoleId == id).ToList();

            db.RoleFunctions.RemoveRange(oldRoleFunctionList);

            db.RoleFunctions.AddRange(roleFunctionList);
            db.SaveChanges();

            ///更新缓存的权限
            cacheService.SetUserPrivilegesByRole(id);
            return Content("ok");
        }

    }
}
