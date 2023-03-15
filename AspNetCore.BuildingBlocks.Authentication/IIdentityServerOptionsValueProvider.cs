namespace AspNetCore.BuildingBlocks.Authentication
{
    public interface IIdentityServerOptionsValueProvider
    {
        string GetScope();
        string GetAuthority();
        string GetApiName();
        bool GetHttpsFlag();
    }
}
