using AspNetCore.BuildingBlocks.Controllers.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.BuildingBlocks.Controllers.HealthChecks
{
    [Route("/api/[controller]/status")]    [ApiController]
    [AllowAnonymous]
    public class HealthCheckController : Controller
    {
        private readonly HealthCheckService _healthCheckService;
        private HealthCheckController(HealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(typeof(HealthCheckReport), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetHealthCheck()
        {
            HealthReport report = await _healthCheckService.CheckHealthAsync();
            HealthCheckReport healthCheckReport = HealthCheckReport.CreateFrom(report);
            if(report.Status == HealthStatus.Healthy || report.Status == HealthStatus.Degraded)
            {
                return Ok(healthCheckReport);
            }

            return StatusCode((int)HttpStatusCode.ServiceUnavailable, healthCheckReport);
        }
    }
}
