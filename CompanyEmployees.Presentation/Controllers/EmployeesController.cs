using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contacts;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers;

[Route("api/companies/{companyId}/employees")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IServiceManager _service;

    public EmployeesController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployeesForCompany(Guid companyId)
    {
        var employees = await _service.EmployeeService.GetEmployeesAsync(companyId, false);

        return Ok(employees);
    }

    [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
    public async Task<IActionResult> GetEmployeeForCompany(Guid companyId, Guid id)
    {
        var employee = await _service.EmployeeService.GetEmployeeAsync(companyId, id, false);

        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployeeForCompany(
        Guid companyId,
        [FromBody] EmployeeForCreationDto? employee)
    {
        if (employee is null)
            return BadRequest("EmployeeForCreationDto object is null");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var employeeToReturn = await _service.EmployeeService.CreateEmployeeForCompanyAsync(
            companyId,
            employee,
            false);

        return CreatedAtRoute(
            "GetEmployeeForCompany",
            new { companyId, id = employeeToReturn.Id },
            employeeToReturn);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid id)
    {
        await _service.EmployeeService.DeleteEmployeeForCompanyAsync(companyId, id, false);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEmployeeForCompany(
        Guid companyId,
        Guid id,
        [FromBody] EmployeeForUpdateDto? employee)
    {
        if (employee is null)
            return BadRequest("EmployeeForUpdateDto object is null");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _service.EmployeeService.UpdateEmployeeForCompanyAsync(companyId, id, employee, false, true);

        return NoContent();
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(
        Guid companyId,
        Guid id,
        [FromBody] JsonPatchDocument<EmployeeForUpdateDto>? patchDoc)
    {
        if (patchDoc is null)
            return BadRequest("Patch document object sent from client is null");

        var (employeeToPatch, employeeEntity) = await _service.EmployeeService.GetEmployeeForPatchAsync(
            companyId,
            id,
            false,
            true);

        patchDoc.ApplyTo(employeeToPatch, ModelState);

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        // prevent an invalid employeeEntity from being saved to the database
        TryValidateModel(employeeToPatch);

        if (!TryValidateModel(employeeToPatch))
            return ValidationProblem(ModelState);

        await _service.EmployeeService.SaveChangesForPatchAsync(employeeToPatch, employeeEntity);

        return NoContent();
    }
}