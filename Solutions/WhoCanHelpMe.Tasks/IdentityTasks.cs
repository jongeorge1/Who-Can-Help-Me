namespace WhoCanHelpMe.Tasks
{
    #region Using Directives

    using System.Security.Authentication;
    using System.Web;
    using System.Web.Security;

    using Domain;
    using Domain.Contracts.Tasks;

    using DotNetOpenAuth.Messaging;
    using DotNetOpenAuth.OpenId;
    using DotNetOpenAuth.OpenId.RelyingParty;

    #endregion

    public class IdentityTasks : IIdentityTasks
    {
        public void Authenticate(string userId)
        {
            var openId = new OpenIdRelyingParty();

            var response = openId.GetResponse();

            if (response == null)
            {
                // Stage 2: user submitting Identifier
                Identifier id;
                if (Identifier.TryParse(userId, out id))
                {
                    try
                    {
                        var redirectingResponse = openId.CreateRequest(userId).RedirectingResponse;
                        redirectingResponse.Send(HttpContext.Current);
                    }
                    catch (ProtocolException ex)
                    {
                        throw new AuthenticationException(ex.Message, ex);
                    }
                }

                throw new AuthenticationException("Invalid identifier");
            }

            // Stage 3: OpenID Provider sending assertion response
            switch (response.Status)
            {
                case AuthenticationStatus.Authenticated:
                    FormsAuthentication.SetAuthCookie(response.ClaimedIdentifier, false);
                    break;

                case AuthenticationStatus.Canceled:
                    throw new AuthenticationException("Cancelled at provider");
 
                case AuthenticationStatus.Failed:
                    throw new AuthenticationException(response.Exception.Message);

                default:
                    throw new AuthenticationException("An unknown problem occurred");
            }
        }

        public Identity GetCurrentIdentity()
        {
            var identity = HttpContext.Current.User.Identity;

            if (!identity.IsAuthenticated)
            {
                return null;
            }

            return new Identity 
                   {
                        UserName = identity.Name
                   };
        }

        public bool IsSignedIn()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}