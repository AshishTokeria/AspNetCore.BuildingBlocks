namespace AspNetCore.BuildingBlocks.Authentication.Client
{
    public interface IIdentityServerTokenReceiver
    {
        Task<string> GetTokenAsync();
    }
}