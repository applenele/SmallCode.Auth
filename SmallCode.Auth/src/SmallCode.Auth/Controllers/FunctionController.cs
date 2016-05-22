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
    public class FunctionController : BaseController
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 分页获取功能
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResult FunctionList(PageRequestModel model)
        {
            IQueryable<Function> query = db.Functions.AsQueryable();
            if (!string.IsNullOrEmpty(model.key))
            {
                query = query.Where(x => x.FunctionName.Contains(model.key));
            }
            if (!string.IsNullOrEmpty(model.sortOrder))
            {
                bool flag = model.sortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase);
                query = query.OrderBy(model.sortField, flag);
            }
            int index = model.pageIndex * model.pageSize;

            List<Function> functions = query.Skip(index).Take(model.pageSize).ToList();
            return Json(functions);
        }


        #region 编辑方法
        /// <summary>
        /// 编辑方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(Guid? id)
        {
            Function function = new Models.Function();
            bool IsInsert = true;
            if (id.HasValue)
            {
                function = db.Functions.First(x => x.Id == id);
                IsInsert = false;
            }
            ViewBag.IsInsert = IsInsert;
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Function function)
        {
            function.CreatedBy = CurrentUser.Id;
            function.CreatedDate = DateTime.Now;
            function.IsDelete = false;
            db.Functions.Add(function);
            db.SaveChanges();
            return View();
        }
        #endregion

        public IActionResult Remove(string ids)
        {
            if (string.IsNullOrEmpty(ids)) return Content("error");
            List<Guid> idList = new List<Guid>();
            ids.Split(',').ToList().ForEach(x => idList.Add(new Guid(x)));
            List<Function> functions = db.Functions.Where(x => idList.Contains(x.Id)).ToList();
            db.Functions.RemoveRange(functions);
            db.SaveChanges();
            return Content("ok");
        }
    }
}
