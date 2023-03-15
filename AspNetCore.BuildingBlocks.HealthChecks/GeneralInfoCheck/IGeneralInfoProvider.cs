namespace AspNetCore.BuildingBlocks.HealthChecks.GeneralInfoCheck
{
    public interface IGeneralInfoProvider
    {
        Dictionary<string, object> GetData();
    }
}
