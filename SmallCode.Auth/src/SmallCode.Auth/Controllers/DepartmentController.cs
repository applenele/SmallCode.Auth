using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.Auth.DataModel;
using SmallCode.Auth.Models;
using SmallCode.Auth.Extensions;
using SmallCode.Auth.Services;

namespace SmallCode.Auth.Controllers
{
    public class DepartmentController : BaseController
    {

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DepartmentList(PageRequestModel model)
        {
            IQueryable<Department> query = db.Departments.AsQueryable();
            if (!string.IsNullOrEmpty(model.key))
            {
                query = query.Where(x => x.DepartmentName.Contains(model.key));
            }
            if (!string.IsNullOrEmpty(model.sortOrder))
            {
                bool flag = model.sortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase);
                query = query.OrderBy(model.sortField, flag);
            }
            int index = model.pageIndex * model.pageSize;
            List<Department> departments = query.Skip(index).Take(model.pageSize).ToList();

            GridResponseData<Department> response = new GridResponseData<Department>();
            response.data = departments;
            response.total = query.Count();
            return Json(response);
        }

        #region 部门信息的编辑
        /// <summary>
        /// 部门信息的编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(Guid? id)
        {
            Department user = new Models.Department();
            bool IsInsert = true;
            if (id.HasValue)
            {
                user = db.Departments.First(x => x.Id == id);
                IsInsert = false;
            }
            ViewBag.IsInsert = IsInsert;
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Department department)
        {
            department.CreatedBy = CurrentUser.Id;
            department.CreatedDate = DateTime.Now;
            department.IsDelete = false;
            db.Departments.Add(department);
            db.SaveChanges();
            return View();
        }
        #endregion


        public IActionResult Remove(string ids)
        {
            if (string.IsNullOrEmpty(ids)) return Content("error");
            List<Guid> idList = new List<Guid>();
            ids.Split(',').ToList().ForEach(x => idList.Add(new Guid(x)));
            List<Department> departments = db.Departments.Where(x => idList.Contains(x.Id)).ToList();
            db.Departments.RemoveRange(departments);
            db.SaveChanges();
            return Content("ok");
        }

    }
}
