namespace AspNetCore.BuildingBlocks.Authentication.Client
{
    public interface IIdentityServerTokenProvider
    {
        string GetClientSecret();
        string GetClientId();
        string GetScopeName();
        string GetIdentityUrl();
    }
}
