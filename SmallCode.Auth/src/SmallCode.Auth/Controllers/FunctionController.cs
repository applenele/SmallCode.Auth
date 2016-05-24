using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.Auth.DataModel;
using SmallCode.Auth.Models;
using SmallCode.Auth.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;

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

            GridResponseData<Function> response = new GridResponseData<Function>();
            response.data = functions;
            response.total = query.Count();
            return Json(response);
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
            WebState state = WebState.Add;
            if (id.HasValue)
            {
                function = db.Functions.First(x => x.Id == id);
                state = WebState.Edit;
            }
            ViewBag.WebState = state;
            return View(function);
        }

        [HttpPost]
        public IActionResult Edit(Function function, WebState State)
        {
            if (WebState.Add == State)
            {
                //增加的逻辑
                function.CreatedBy = CurrentUser.Id;
                function.CreatedDate = DateTime.Now;
                function.IsDelete = false;
                db.Functions.Add(function);
                db.SaveChanges();
                return Content("ok");
            }
            else if (WebState.Edit == State)
            {
                ///修改的逻辑
                Function old = new Function();
                old = db.Functions.Where(x => x.Id == function.Id).FirstOrDefault();
                if (old == null)
                {
                    return Content("ok");
                }
                else
                {
                    old.FunctionCode = function.FunctionCode;
                    old.FunctionName = function.FunctionName;
                    old.Url = function.Url;
                    old.FatherId = function.FatherId;
                    db.SaveChanges();
                    return Content("ok");
                }
            }

            return Content("error");
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


        [HttpGet]
        public IActionResult GetFathers()
        {
            List<Function> functions = db.Functions.Where(x => x.Url == "#").ToList();
            return Json(functions);
        }
    }
}
