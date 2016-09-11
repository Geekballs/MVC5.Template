using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using App.Web.Lib.Attributes;
using App.Web.Lib.Data.Services;
using App.Web.Lib.ViewModels;
using X.PagedList;

namespace App.Web.Lib.Controllers
{
    [RoutePrefix("Admin")]
    [Trust(Privilege = "Admin")]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        #region Index

        [Route("Roles"), HttpGet]
        public ActionResult Index(string term, int? page)
        {
            var model = _roleService.GetAllRoles().Select(r => new RoleVm.Index()
            {
                RoleId = r.RoleId,
                RoleName = r.Name,
                RoleDescription = r.Description,
                RoleUserCount = r.UserRoles.Count
            });
            if (!string.IsNullOrEmpty(term))
            {
                model = model.Where(r => r.RoleName.Contains(term) || r.RoleDescription.Contains(term.ToLower()));
            }
            var pageNo = page ?? 1;
            var pagedData = model.ToPagedList(pageNo, AppConfig.PageSize);
            ViewBag.Data = pagedData;
            return View("Index", pagedData);
        }

        #endregion

        #region Detail 

        [Route("Role-Detail/{id}"), HttpGet]
        public ActionResult Detail(Guid id)
        {
            var role = _roleService.GetById(id);
            if (role == null)
            {
                GetAlert(Danger, "Role cannot be found!");
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = new RoleVm.Detail()
            {
                RoleId = role.RoleId,
                RoleName = role.Name,
                RoleDescription = role.Description
            };
            var roleUsers = _roleService.GetUsersInRole(id);
            var userDetail = roleUsers.Select(ru => new RoleVm.RoleUsersDetail()
            {
                UserId = ru.UserId,
                UserName = ru.User.UserName
            }).ToList();
            model.RoleUsersDetail = userDetail;
            return View("Detail", model);
        }

        #endregion

        #region Create

        [Route("Create-Role"), HttpGet]
        public ActionResult Create()
        {
            var model = new RoleVm.Create();
            return View("Create", model);
        }

        [Route("Create-Role"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(RoleVm.Create model)
        {
            if (ModelState.IsValid)
            {
                _roleService.CreateRole(model.RoleName, model.RoleDescription);
                GetAlert(Success, "Role created!");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Error!");
            return View("Create", model);
        }

        #endregion

        #region Edit

        [Route("Edit-Role/{id}"), HttpGet]
        public ActionResult Edit(Guid id)
        {
            var role = _roleService.GetById(id);
            if (role == null)
            {
                GetAlert(Danger, "Role cannot be found!");
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = new RoleVm.Edit()
            {
                RoleId = role.RoleId,
                RoleName = role.Name,
                RoleDescription = role.Description
            };
            return View("Edit", model);
        }

        [Route("Edit-Role/{id}"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(RoleVm.Edit model)
        {
            if (ModelState.IsValid)
            {
                _roleService.EditRole(model.RoleId, model.RoleName, model.RoleDescription);
                GetAlert(Success, "Role updated!");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Error!");
            return View("Edit", model);
        }

        #endregion

        #region Delete

        [Route("Delete-Role/{id}"), HttpGet]
        public ActionResult Delete(Guid id)
        {
            var role = _roleService.GetById(id);
            if (role == null)
            {
                GetAlert(Danger, "Role cannot be found!");
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = new RoleVm.Delete()
            {
                RoleId = role.RoleId,
                RoleName = role.Name,
                RoleDescription = role.Description
            };
            return View("Delete", model);
        }

        [Route("Delete-Role/{id}"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(RoleVm.Delete model)
        {
            if (ModelState.IsValid)
            {
                _roleService.DeleteRole(model.RoleId);
                GetAlert(Success, "Role deleted!");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Error!");
            return View("Delete", model);
        }

        #endregion
    }
}