using Csla;
using Csla.Core;
using Csla.Security;
using System.Security.Claims;

namespace CslaMaui.BusinessLayer
{
    [Serializable]
    public class CredentialValidator : ReadOnlyBase<CredentialValidator>
    {
        public static readonly PropertyInfo<string> UsernameProperty = RegisterProperty<string>(nameof(Username));
        public string Username
        {
            get => GetProperty(UsernameProperty);
            private set => LoadProperty(UsernameProperty, value);
        }

        public static readonly PropertyInfo<string> AuthenticationTypeProperty = RegisterProperty<string>(nameof(AuthenticationType));
        public string AuthenticationType
        {
            get => GetProperty(AuthenticationTypeProperty);
            private set => LoadProperty(AuthenticationTypeProperty, value);
        }

        public ClaimsPrincipal GetPrincipal()
        {
            var identity = new ClaimsIdentity(AuthenticationType);
            if (!string.IsNullOrWhiteSpace(Username))
            {
                identity.AddClaim(new Claim(ClaimTypes.Name, Username));
            }
            return new ClaimsPrincipal(identity);
        }

        [Fetch]
        [RunLocal]
        private void Fetch(Credentials credentials)
        {
            if (credentials.Password == "password")
            {
                Username = credentials.Username;
                AuthenticationType = "Custom";
            }
        }
    }
}
