using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using App.Web.Lib.ViewModels;
using X.PagedList;

namespace App.Web.Lib.Controllers
{
    [RoutePrefix("Role-Admin")]
    public class RoleController : BaseController
    {
        #region Index

        [Route("All"), HttpGet]
        public ActionResult Index(string term, int? page)
        {
            var model = TheRoleManager.GetAllRoles().Select(r => new RoleVm.Index()
            {
                RoleId = r.RoleId,
                RoleName = r.Name,
                RoleDescription = r.Description,
                RoleUserCount = r.UserRoles.Count,
                RoleLocked = r.Locked,
                RoleEnabled = r.Enabled
            });
            if (!string.IsNullOrEmpty(term))
            {
                model = model.Where(r => r.RoleName.Contains(term) || r.RoleDescription.Contains(term));
            }
            var pageNo = page ?? 1;
            var pagedData = model.ToPagedList(pageNo, AppConfig.PageSize);
            ViewBag.Data = pagedData;
            return View("Index", pagedData);
        }

        #endregion

        #region Detail 

        [Route("Detail/{id}"), HttpGet]
        public ActionResult Detail(Guid? id)
        {
            if (id == null)
            {
                GetAlert(Danger, "ID cannot e null!");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = TheRoleManager.GetRoleById(id);
            if (role == null)
            {
                GetAlert(Danger, "Role cannot be found!");
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = new RoleVm.Detail()
            {
                RoleId = role.RoleId,
                RoleName = role.Name,
                RoleDescription = role.Description,
                RoleEnabled = role.Enabled,
                RoleLocked = role.Locked
            };
            var roleUsers = TheRoleManager.GeUsersInRole(id);
            var userDetail = roleUsers.Select(ru => new RoleVm.RoleUsersDetail()
            {
                UserId = ru.UserId,
                UserName = ru.User.Name

            }).ToList();
            model.RoleUsersDetail = userDetail;
            return View("Detail", model);
        }

        #endregion

        #region Create

        [Route("Create"), HttpGet]
        public ActionResult Create()
        {
            var model = new RoleVm.Create();
            return View("Create", model);
        }

        [Route("Create"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(RoleVm.Create model)
        {
            if (ModelState.IsValid)
            {
                TheRoleManager.CreateRole(model.RoleName, model.RoleDescription, model.RoleEnabled, model.RoleLocked);
                GetAlert(Success, "Role created!");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Error!");
            return View("Create", model);
        }

        #endregion

        #region Update

        [Route("Edit/{id}"), HttpGet]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                GetAlert(Danger, "ID cannot e null!");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = TheRoleManager.GetRoleById(id);
            if (role == null)
            {
                GetAlert(Danger, "Role cannot be found!");
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = new RoleVm.Edit()
            {
                RoleId = role.RoleId,
                RoleName = role.Name,
                RoleDescription = role.Description,
                RoleEnabled = role.Enabled,
                RoleLocked = role.Locked
            };
            return View("Edit", model);
        }

        [Route("Edit/{id}"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(RoleVm.Edit model)
        {
            if (ModelState.IsValid)
            {
                TheRoleManager.EditRole(model.RoleId, model.RoleName, model.RoleDescription, model.RoleEnabled, model.RoleLocked);
                GetAlert(Success, "Role updated!");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Error!");
            return View("Edit", model);
        }

        #endregion

        #region Delete

        [Route("Delete/{id}"), HttpGet]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                GetAlert(Danger, "ID cannot be null!");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = TheRoleManager.GetRoleById(id);
            if (role == null)
            {
                GetAlert(Danger, "Role cannot be found!");
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = new RoleVm.Delete()
            {
                RoleId = role.RoleId,
                RoleName = role.Name,
                RoleDescription = role.Description,
                RoleEnabled = role.Enabled,
                RoleLocked = role.Locked
            };
            return View("Delete", model);
        }

        [Route("Delete/{id}"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(RoleVm.Delete model)
        {
            if (ModelState.IsValid)
            {
                TheRoleManager.DeleteRole(model.RoleId);
                GetAlert(Success, "Role deleted!");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Error!");
            return View("Delete", model);
        }

        #endregion
    }
}