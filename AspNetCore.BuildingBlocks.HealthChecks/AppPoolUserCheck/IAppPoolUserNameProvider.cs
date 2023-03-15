namespace AspNetCore.BuildingBlocks.HealthChecks.AppPoolUserCheck
{
    public interface IAppPoolUserNameProvider
    {
        string GetUserName();
    }
}
