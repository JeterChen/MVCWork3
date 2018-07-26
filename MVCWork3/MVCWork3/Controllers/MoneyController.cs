using MVCWork3.Enums;
using MVCWork3.Models.ViewModels;
using MVCWork3.Repository;
using MVCWork3.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MVCWork3.Controllers
{
    [Authorize]
    public class MoneyController : Controller
    {
        private readonly MoneyService _imoneyService;
        private readonly UnitOfWork _unitOfWork;

        public MoneyController()
        {
            _unitOfWork = new UnitOfWork();
            _imoneyService = new MoneyService(_unitOfWork);
        }

        // GET: Money
        public ActionResult Index()
        {
            ViewData["CategoryItems"] = GetCategoryList();
            ViewData["PageItems"] = GetPageDropDownList();
            ViewData["currentPage"] = 1;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Type,Date,Price,Description")]
                                   MoneyViewModel model)
        {

            ViewData["CategoryItems"] = GetCategoryList();
            ViewData["PageItems"] = GetPageDropDownList();
            /***不知道為什麼model裡面的Type都是None???***/
            if (ModelState.IsValid)
            {
                _imoneyService.Add(model);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(model);
        }

       // [ChildActionOnly]
        public ActionResult ListAction(int page)
        {
            int pagesize = 10;
            var result = _imoneyService.LookupByPageList(page, pagesize);

            ViewData["currentPage"] = page;

            return View(result);
        }

        private SelectList GetPageDropDownList()
        {
            int pagesize = 10;
            var sources = _imoneyService.LookupAllData();

            var pageResult = sources.Select((item, inx) => new { item, inx })
                                 .GroupBy(x => x.inx / pagesize)
                                 .Select(g => g.Select(s => s.item));

            var _resources = pageResult.Select((p,inx) => new PageDropDownListViewModel
            {
                name = inx + 1,
                value = inx
            });


            return new SelectList(_resources,"value","name",0);


        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid? id)
        {
            ViewData["CategoryItems"] = GetCategoryList();

            if (id.HasValue == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var EditVo = _imoneyService.GetSingleData(id.Value);
            if (EditVo == null)
            {
                return HttpNotFound();
            }
            return View(EditVo);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type,Date,Price,Description")]
                                   MoneyViewModel model)
        {
            if (ModelState.IsValid)
            {
                _imoneyService.Edit(model.Id, model);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        private IList<SelectListItem> GetCategoryList()
        {
            return EnumHelper.GetSelectList(typeof(CategoryType));
        }


        public ActionResult Details(Guid? id)
        {
            if (id.HasValue == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var detail = _imoneyService.GetSingleData(id.Value);

            if (detail == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid? id)
        {
            if (id.HasValue == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var detail = _imoneyService.GetSingleData(id.Value);

            if (detail == null)
            {
                return HttpNotFound();
            }

            return View(detail);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _imoneyService.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }

    }
}