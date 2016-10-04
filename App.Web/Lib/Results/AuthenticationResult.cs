namespace App.Web.Lib.Results
{
    public class AuthenticationResult
    {
        public AuthenticationResult(string errMessage = null)
        {
            ErrorMessage = errMessage;
        }

        public string ErrorMessage { get; private set; }
        public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);
    }
}