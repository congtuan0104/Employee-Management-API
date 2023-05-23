using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Service.Contacts;
using Shared.RequestFeatures;

namespace CompanyEmployees.Presentation.Controllers;

[ApiVersion("2.0", Deprecated = true)]
[Route("api/companies2")]
[ApiController]
public class CompaniesV2Controller : ControllerBase
{
    private readonly IServiceManager _service;

    public CompaniesV2Controller(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet(Name = "GetCompaniesV2")]
    public async Task<IActionResult> GetCompanies([FromQuery] CompanyParameters companyParameters)
    {
        var pagedResult = await _service.CompanyService
            .GetAllCompaniesAsync(companyParameters, false);

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

        var returnCompanies = pagedResult.companies.Select(x => $"{x.Name} V2");

        return Ok(returnCompanies);
    }
}