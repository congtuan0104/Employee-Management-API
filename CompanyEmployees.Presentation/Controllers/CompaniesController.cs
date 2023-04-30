using Microsoft.AspNetCore.Mvc;
using Service.Contacts;

namespace CompanyEmployees.Presentation;

[Route("api/companies")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly IServiceManager _service;

    public CompaniesController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetCompanies()
    {
        try
        {
            var companies = _service.CompanyService.GetAllCompanies(true);
            return Ok(companies);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error");
        }
    }
}