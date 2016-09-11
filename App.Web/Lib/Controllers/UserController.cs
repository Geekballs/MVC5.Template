using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using App.Web.Lib.Attributes;
using App.Web.Lib.Data.Services;
using App.Web.Lib.Models;
using App.Web.Lib.ViewModels;
using X.PagedList;

namespace App.Web.Lib.Controllers
{
    [RoutePrefix("Admin")]
    [Trust(Privilege = "Admin")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        #region Index

        [Route("Users"), HttpGet]
        public ActionResult Index(string term, int? page)
        {
            var model = _userService.GetAllUsers().Select(u => new UserVm.Index()
            {
                UserId = u.UserId,
                UserName = u.UserName,
                UserFirstName = u.FirstName,
                UserLastName = u.LastName,
                UserAlias = u.Alias,
                EmailAddress = u.EmailAddress,
                LoginEnabled = u.LoginEnabled,
                UserRoleCount = u.UserRoles.Count
            });
            if (!string.IsNullOrEmpty(term))
            {
                model = model.Where(s => s.UserName.Contains(term.ToLower()));
            }
            var pageNo = page ?? 1;
            var pagedData = model.ToPagedList(pageNo, AppConfig.PageSize);
            ViewBag.Data = pagedData;

            return View("Index", pagedData);
        }

        #endregion

        #region Detail 

        [Route("User-Detail/{id}"), HttpGet]
        public ActionResult Detail(Guid id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                GetAlert(Danger, "User cannot be found!");
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = new UserVm.Detail()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserFirstName = user.FirstName,
                UserLastName = user.LastName,
                UserAlias = user.Alias,
                UserEmailAddress = user.EmailAddress,
                UserLoginEnabled = user.LoginEnabled
            };
            var userRoles = _userService.GetRolesForUser(id);
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

        [Route("Create-User"), HttpGet]
        public ActionResult Create()
        {
            var model = new UserVm.Create();
            var roles = _roleService.GetAllRoles();;
            var roleDetail = roles.Select(rd => new CheckBoxListItem()
            {
                Id = rd.RoleId,
                Display = rd.Name,
                IsChecked = false
            }).ToList();
            model.Roles = roleDetail;
            return View("Create", model);
        }

        [Route("Create-User"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(UserVm.Create model)
        {
            if (ModelState.IsValid)
            {
                var rolesToAdd = model.Roles.Where(r => r.IsChecked).Select(r => r.Id).ToList();
                _userService.CreateUser(model.UserName, model.UserFirstName, model.UserLastName, model.UserAlias, model.UserEmailAddress, model.UserLoginEnabled, rolesToAdd);
                GetAlert(Success, "User created!");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Error!");
            return View("Create", model);
        }

        #endregion

        #region Edit

        [Route("Edit-User/{id}"), HttpGet]
        public ActionResult Edit(Guid id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                GetAlert(Danger, "User cannot be found!");
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = new UserVm.Edit()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserFirstName = user.FirstName,
                UserLastName = user.LastName,
                UserAlias = user.Alias,
                UserEmailAddress = user.EmailAddress,
                UserLoginEnabled = user.LoginEnabled
            };
            var userRoles = _userService.GetRolesForUser(id);
            var roles = _roleService.GetAllRoles();
            var roleDetail = roles.Select(rd => new CheckBoxListItem()
            {
                Id = rd.RoleId,
                Display = rd.Name,
                IsChecked = userRoles.Any(ur => ur.RoleId == rd.RoleId)
            }).ToList();
            model.Roles = roleDetail;
            return View("Edit", model);
        }

        [Route("Edit-User/{id}"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(UserVm.Edit model)
        {
            if (ModelState.IsValid)
            {
                var rolesToAdd = model.Roles.Where(r => r.IsChecked).Select(r => r.Id).ToList();
                _userService.EditUser(model.UserId, model.UserName, model.UserFirstName, model.UserLastName, model.UserAlias, model.UserEmailAddress, model.UserLoginEnabled, rolesToAdd);
                GetAlert(Success, "User updated!");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Error!");
            return View("Edit", model);
        }

        #endregion

        #region Delete

        [Route("Delete-User/{id}"), HttpGet]
        public ActionResult Delete(Guid id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                GetAlert(Danger, "User cannot be found!");
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = new UserVm.Delete()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserFirstName = user.FirstName,
                UserLastName = user.LastName,
                UserAlias = user.Alias,
                EmailAddress = user.EmailAddress,
                UserLoginEnabled = user.LoginEnabled
            };
            return View("Delete", model);
        }

        [Route("Delete-User/{id}"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(UserVm.Delete model)
        {
            if (ModelState.IsValid)
            {
                _userService.DeleteUser(model.UserId);
                GetAlert(Success, "User deleted!");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Error!");
            return View("Delete", model);
        }

        #endregion
    }
}
