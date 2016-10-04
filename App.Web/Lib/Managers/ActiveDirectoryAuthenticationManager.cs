using System;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using App.Web.Lib.Results;
using Microsoft.Owin.Security;

namespace App.Web.Lib.Managers
{
    public class ActiveDirectoryAuthenticationManager
    {
        private readonly IAuthenticationManager _authMgr;
        private readonly ApplicationAuthenticationManager _appAuthMgr = new ApplicationAuthenticationManager();

        public ActiveDirectoryAuthenticationManager(IAuthenticationManager authMgr)
        {
            _authMgr = authMgr;
        }

        public AuthenticationResult SignIn(string username, string password)
        {
            #if DEBUG
            var authType = ContextType.Machine;

            #else
            // authenticates against your Domain AD
            var authType = ContextType.Domain;

            #endif
            var principalCtx = new PrincipalContext(authType);
            var isAuthed = false;
            UserPrincipal userPrincipal = null;
            try
            {
                isAuthed = principalCtx.ValidateCredentials(username, password, ContextOptions.Negotiate);
                if (isAuthed && !_appAuthMgr.DoesUserExist(username) && AppConfig.AutoRegister.Equals(true))
                {
                    // TODO: Get SAM Account details!
                    var firstName = "";
                    var lastName = "";
                    var email = "";
                    var alias = firstName + " " + lastName;
                    bool loginEnabled = true;

                    _appAuthMgr.CreateUser(username, firstName, lastName, email, alias, loginEnabled);
                }
                if (isAuthed && _appAuthMgr.GetUserByName(username).LoginEnabled)
                {
                    userPrincipal = UserPrincipal.FindByIdentity(principalCtx, username);
                }
            }
            catch (Exception)
            {
                isAuthed = false;
                userPrincipal = null;
            }

            // The user has been authenticated, but they are not enabled in this application.
            if (isAuthed && !_appAuthMgr.GetUserByName(username).LoginEnabled)
            {
                return new AuthenticationResult("Unauthorized Application Access!");
            }

            if (!isAuthed || userPrincipal == null)
            {
                return new AuthenticationResult("Incorrect Credentials!");
            }

            if (userPrincipal.IsAccountLockedOut())
            {
                return new AuthenticationResult("AD Account Locked!");
            }

            if (userPrincipal.Enabled.HasValue && userPrincipal.Enabled.Value == false)
            {
                return new AuthenticationResult("AD Account Disabled!");
            }
            var identity = CreateIdentity(userPrincipal);
            _authMgr.SignOut(MyAuthentication.ApplicationCookie);
            _authMgr.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);
            return new AuthenticationResult();
        }

        private ClaimsIdentity CreateIdentity(UserPrincipal userPrincipal)
        {
            var identity = new ClaimsIdentity(MyAuthentication.ApplicationCookie, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "Active Directory"));
            identity.AddClaim(new Claim(ClaimTypes.Name, userPrincipal.SamAccountName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userPrincipal.SamAccountName));
            if (!string.IsNullOrEmpty(userPrincipal.EmailAddress))
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, userPrincipal.EmailAddress));
            }

            var user = _appAuthMgr.GetUserByName(userPrincipal.SamAccountName);
            var userRoles = ApplicationAuthenticationManager.GetRolesForUser(user.UserId);

            foreach (var role in userRoles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role.Role.Name));
            }

            return identity;
        }
    }
}