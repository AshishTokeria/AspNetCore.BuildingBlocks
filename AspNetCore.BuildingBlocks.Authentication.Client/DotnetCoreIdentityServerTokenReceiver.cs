namespace AspNetCore.BuildingBlocks.Authentication.Client
{
    public class DotnetCoreIdentityServerTokenReceiver : BaseIdentityServerTokenReceiver
    {
        public DotnetCoreIdentityServerTokenReceiver(IIdentityServerTokenProvider configProvier,
            HttpClient httpClient) : base(
                configProvier.GetIdentityUrl(),
                configProvier.GetClientSecret(),
                configProvier.GetClientId(),
                configProvier.GetScopeName(),
                httpClient)
        {

        }
    }
}
