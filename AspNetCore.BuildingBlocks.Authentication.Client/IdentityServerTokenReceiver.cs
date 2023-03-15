namespace AspNetCore.BuildingBlocks.Authentication.Client
{
    public class IdentityServerTokenReceiver : BaseIdentityServerTokenReceiver
    {
        public IdentityServerTokenReceiver(string identityUrl, string clientSecret, string clientid, string scopeName) :
            base(identityUrl, clientSecret, clientid, scopeName, new HttpClient())
        { 
        }
    }
}
