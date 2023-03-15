namespace AspNetCore.BuildingBlocks.Authentication.Client
{
    public class DotnetCoreAuthenticationHandler : BaseAuthenticationHandler
    {
        public DotnetCoreAuthenticationHandler(IdentityServerTokenReceiver tokenReceiver)
            :base(tokenReceiver) 
        {
        }
    }
}
