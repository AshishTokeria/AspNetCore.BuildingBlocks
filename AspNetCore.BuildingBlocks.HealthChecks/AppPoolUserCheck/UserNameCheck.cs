using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.BuildingBlocks.HealthChecks.AppPoolUserCheck
{
    public class UserNameCheck : IHealthCheck
    {
        private readonly IAppPoolUserNameProvider _userNameProvider;

        public UserNameCheck(IAppPoolUserNameProvider userNameProvider)
        {
            _userNameProvider = userNameProvider ?? throw new ArgumentNullException(nameof(userNameProvider));
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                string actualUserName = Environment.UserName;
                string expectedUserName = _userNameProvider.GetUserName();

                bool isUserCorrect = string.Equals(
                    actualUserName,
                    expectedUserName,
                    StringComparison.InvariantCultureIgnoreCase);

                HealthCheckResult result = isUserCorrect
                    ? new HealthCheckResult(
                        HealthStatus.Healthy,
                        "UserName check passed",
                        null,
                        new Dictionary<string, object>
                        {
                        {"ActualUserName", actualUserName },
                        { "ExpectedUserName", expectedUserName }
                        })
                    : new HealthCheckResult(
                        HealthStatus.Unhealthy,
                        "UserName check NOT passed",
                        null,
                        new Dictionary<string, object>
                        {
                        {"ActualUserName", actualUserName },
                        { "ExpectedUserName", expectedUserName }
                        });

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                return Task.FromResult(
                    new HealthCheckResult(
                        HealthStatus.Unhealthy,
                        "UserName check failed",
                        ex,
                        new Dictionary<string, object>
                        {
                            { "ExceptionMessage", ex.Message}
                        }));
            }
        }
    }
}
