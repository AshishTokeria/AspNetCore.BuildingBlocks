namespace AspNetCore.BuildingBlocks.Authentication.Client
{
    internal class AuthenticationHandler : BaseAuthenticationHandler
    {
        public AuthenticationHandler(IIdentityServerTokenReceiver tokenReceiver) 
            : base(tokenReceiver, new HttpClientHandler())
        {

        }
    }
}
