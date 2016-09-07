using System;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using App.Web.Lib.Data.Managers;
using Microsoft.Owin.Security;

namespace App.Web.Lib.Services
{
    public class AdAuthenticationService
    {
        AuthManager TheAuthManager = new AuthManager();

        public class AuthenticationResult
        {
            public AuthenticationResult(string errMessage = null)
            {
                ErrorMessage = errMessage;
            }

            public string ErrorMessage { get; private set; }
            public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);
        }

        private readonly IAuthenticationManager _authManager;

        public AdAuthenticationService(IAuthenticationManager authManager)
        {
            _authManager = authManager;
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
                if (isAuthed && TheAuthManager.IsEnabled(username))
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
            if (isAuthed && TheAuthManager.IsEnabled(username))
            {
                return new AuthenticationResult("Unauthorized!");
            }

            if (!isAuthed || userPrincipal == null)
            {
                return new AuthenticationResult("Username or Password is not correct");
            }

            if (userPrincipal.IsAccountLockedOut())
            {
                return new AuthenticationResult("Your account is locked.");
            }

            if (userPrincipal.Enabled.HasValue && userPrincipal.Enabled.Value == false)
            {
                return new AuthenticationResult("Your account is disabled");
            }
            var identity = CreateIdentity(userPrincipal);
            _authManager.SignOut(MyAuthentication.ApplicationCookie);
            _authManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);
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

            // Add more claims to the cookie here...

            return identity;
        }
    }
}