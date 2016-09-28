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
        private readonly IUserService _userSvc;
        private readonly IRoleService _roleSvc;
        private readonly ITeamService _teamSvc;

        public UserController(IUserService userSvc, IRoleService roleSvc, ITeamService teamSvc)
        {
            _userSvc = userSvc;
            _roleSvc = roleSvc;
            _teamSvc = teamSvc;
        }

        #region Index

        [Route("Users"), HttpGet]
        public ActionResult Index(string term, int? page)
        {
            var model = _userSvc.GetAll().Select(vm => new UserVm.Index
            {
                UserId = vm.UserId,
                UserName = vm.UserName,
                UserFirstName = vm.FirstName,
                UserLastName = vm.LastName,
                UserAlias = vm.Alias,
                EmailAddress = vm.EmailAddress,
                LoginEnabled = vm.LoginEnabled,
                UserRoleCount = vm.UserRoles.Count
            });
            if (!string.IsNullOrEmpty(term))
            {
                model = model.Where(p => p.UserName.Contains(term.ToLower()));
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
            var user = _userSvc.GetById(id);
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
            var userRolesList = _userSvc.GetRolesForUser(id).Select(vm => new UserVm.UserRoles
            {
                RoleId = vm.RoleId,
                RoleName = vm.Role.Name
            }).ToList();
            var userTeamsList = _userSvc.GetTeamsForUser(id).Select(vm => new UserVm.UserTeams
            {
                TeamId = vm.TeamId,
                TeamName = vm.Team.Name
            }).ToList();
            model.UserRolesList = userRolesList;
            model.UserTeamsList = userTeamsList;
            return View("Detail", model);
        }

        #endregion

        #region Create

        [Route("Create-User"), HttpGet]
        public ActionResult Create()
        {
            var model = new UserVm.Create();
            var rolesList = _roleSvc.GetAll().Select(p => new CheckBoxListItem
            {
                Id = p.RoleId,
                Display = p.Name,
                IsChecked = false
            }).ToList();
            var teamsList = _teamSvc.GetAll().Select(p => new CheckBoxListItem
            {
                Id = p.TeamId,
                Display = p.Name,
                IsChecked = false
            }).ToList();
            model.RolesList = rolesList;
            model.TeamsList = teamsList;
            return View("Create", model);
        }

        [Route("Create-User"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(UserVm.Create model)
        {
            if (ModelState.IsValid)
            {
                var rolesToAdd = model.RolesList.Where(p => p.IsChecked).Select(r => r.Id).ToList();
                var teamsToAdd = model.TeamsList.Where(p => p.IsChecked).Select(r => r.Id).ToList();
                _userSvc.Create(model.UserName, model.UserFirstName, model.UserLastName, model.UserAlias, model.UserEmailAddress, model.UserLoginEnabled, rolesToAdd, teamsToAdd);
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
            var user = _userSvc.GetById(id);
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
            var userRoles = _userSvc.GetRolesForUser(id);
            var rolesList = _roleSvc.GetAll().Select(vm => new CheckBoxListItem
            {
                Id = vm.RoleId,
                Display = vm.Name,
                IsChecked = userRoles.Any(p => p.RoleId == vm.RoleId)
            }).ToList();
            var userTeams = _userSvc.GetTeamsForUser(id);
            var teamsList = _teamSvc.GetAll().Select(vm => new CheckBoxListItem
            {
                Id = vm.TeamId,
                Display = vm.Name,
                IsChecked = userTeams.Any(p => p.TeamId == vm.TeamId)
            }).ToList();
            model.RolesList = rolesList;
            model.TeamsList = teamsList;
            return View("Edit", model);
        }

        [Route("Edit-User/{id}"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(UserVm.Edit model)
        {
            if (ModelState.IsValid)
            {
                var rolesToAdd = model.RolesList.Where(p => p.IsChecked).Select(r => r.Id).ToList();
                var teamsToAdd = model.TeamsList.Where(p => p.IsChecked).Select(r => r.Id).ToList();
                _userSvc.Edit(model.UserId, model.UserName, model.UserFirstName, model.UserLastName, model.UserAlias, model.UserEmailAddress, model.UserLoginEnabled, rolesToAdd, teamsToAdd);
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
            var user = _userSvc.GetById(id);
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
                _userSvc.Delete(model.UserId);
                GetAlert(Success, "User deleted!");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Error!");
            return View("Delete", model);
        }

        #endregion
    }
}
