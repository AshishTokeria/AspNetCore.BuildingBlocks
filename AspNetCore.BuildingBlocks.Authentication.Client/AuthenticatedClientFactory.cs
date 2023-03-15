namespace AspNetCore.BuildingBlocks.Authentication.Client
{
    public class AuthenticatedClientFactory
    {
        public HttpClient GetAuthrnticatedHttpClient(string serviceUrl, string identityUrl, string clientSecret,
            string clientId)
        {
            return GetAuthrnticatedHttpClient(serviceUrl, identityUrl, clientSecret, clientId, "");
        }

        public HttpClient GetAuthrnticatedHttpClient(string serviceUrl, string identityUrl, string clientSecret,
            string clientId, string scopeName)
        {
            IdentityServerTokenReceiver tokenReceiver = 
                new IdentityServerTokenReceiver(identityUrl, clientSecret, clientId, scopeName);
            return GetAuthenticatedHttpClient(serviceUrl, tokenReceiver);
        }

        public HttpClient GetAuthenticatedHttpClient(string serviceUrl, IIdentityServerTokenReceiver tokenReceiver)
        {
            AuthenticationHandler httpMessageHandler = new AuthenticationHandler(tokenReceiver);

            HttpClient client = new HttpClient(httpMessageHandler)
            {
                BaseAddress = new Uri(serviceUrl)
            };
            return client;
        }
    }
}
