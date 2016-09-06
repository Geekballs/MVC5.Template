using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using App.Web.Lib.Data.Contexts;
using App.Web.Lib.Data.Entities;
using App.Web.Lib.Models;
using App.Web.Lib.ViewModels;
using X.PagedList;

namespace App.Web.Lib.Controllers
{
    [RoutePrefix("User-Admin")]
    public class UserController : BaseController
    {
        #region Index

        [Route("All"), HttpGet]
        public ActionResult Index(string term, int? page)
        {
            var model = TheUserManager.GetAllRoles().Select(u => new UserVm.Index()
            {
                UserId = u.UserId,
                UserName = u.Name,
                UserRoleCount = u.UserRoles.Count,
                UserLocked = u.Locked,
                UserEnabled = u.Enabled
            });
            if (!string.IsNullOrEmpty(term))
            {
                model = model.Where(s => s.UserName.Contains(term));
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
            var user = TheUserManager.GetUserById(id);
            if (user == null)
            {
                GetAlert(Danger, "User cannot be found!");
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = new UserVm.Detail()
            {
                UserId = user.UserId,
                UserName = user.Name,
                UserEnabled = user.Enabled,
                UserLocked = user.Locked
            };

            var userRoles = TheUserManager.GetRolesForUser(id);
            var roleDetail = userRoles.Select(rd => new UserVm.UserRolesDetail()
            {
                RoleId = rd.RoleId,
                RoleName = rd.Role.Name

            }).ToList();

            model.UserRolesDetail = roleDetail;
            return View("Detail", model);
        }

        #endregion

        #region Create

        [Route("Create"), HttpGet]
        public ActionResult Create()
        {
            var model = new UserVm.Create();
            var roles = TheRoleManager.GetAllRoles();
            var roleDetail = roles.Select(rd => new CheckBoxListItem()
            {
                Id = rd.RoleId,
                Display = rd.Name,
                IsChecked = false

            }).ToList();
            model.Roles = roleDetail;
            return View("Create", model);
        }

        [Route("Create"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(UserVm.Create model)
        {
            if (ModelState.IsValid)
            {
                var rolesToAdd = model.Roles.Where(r => r.IsChecked).Select(r => r.Id).ToList();
                TheUserManager.CreateUser(model.UserName, model.UserEnabled, model.UserLocked, rolesToAdd);
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
                GetAlert(Danger, "ID cannot be null!");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = TheUserManager.GetUserById(id);
            if (user == null)
            {
                GetAlert(Danger, "User cannot be found!");
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = new UserVm.Edit()
            {
                UserId = user.UserId,
                UserName = user.Name,
                UserEnabled = user.Enabled,
                UserLocked = user.Locked
            };
            var userRoles = TheUserManager.GetRolesForUser(id);
            var roles = TheRoleManager.GetAllRoles();
            var roleDetail = roles.Select(rd => new CheckBoxListItem()
            {
                Id = rd.RoleId,
                Display = rd.Name,
                IsChecked = userRoles.Any(ur => ur.RoleId == rd.RoleId)

            }).ToList();
            model.Roles = roleDetail;
            return View("Edit", model);
        }

        [Route("Edit/{id}"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(UserVm.Edit model)
        {
            if (ModelState.IsValid)
            {
                var rolesToAdd = model.Roles.Where(r => r.IsChecked).Select(r => r.Id).ToList();
                TheUserManager.EditUser(model.UserId, model.UserName, model.UserEnabled, model.UserLocked, rolesToAdd);
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
            var user = TheUserManager.GetUserById(id);
            if (user == null)
            {
                GetAlert(Danger, "User cannot be found!");
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = new UserVm.Delete()
            {
                UserId = user.UserId,
                UserName = user.Name,
                UserEnabled = user.Enabled,
                UserLocked = user.Locked
            };
            return View("Delete", model);
        }

        [Route("Delete/{id}"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(UserVm.Delete model)
        {
            if (ModelState.IsValid)
            {
                TheUserManager.DeleteUser(model.UserId);
                GetAlert(Success, "User deleted!");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Error!");
            return View("Delete", model);
        }

        #endregion
    }
}
