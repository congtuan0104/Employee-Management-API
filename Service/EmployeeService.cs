using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contacts;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class EmployeeService : IEmployeeService
{
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repository;

    public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, bool trackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);

        if (company is null)
            throw new CompanyNotFoundException(companyId);

        var employees = await _repository.Employee.GetEmployeesAsync(companyId, trackChanges);

        return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
    }

    public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);

        if (company is null)
            throw new CompanyNotFoundException(companyId);

        var employee = await _repository.Employee.GetEmployeeAsync(companyId, employeeId, trackChanges);

        if (employee is null)
            throw new EmployeeNotFoundException(employeeId);

        return _mapper.Map<EmployeeDto>(employee);
    }

    public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(
        Guid companyId,
        EmployeeForCreationDto employeeForCreation,
        bool trackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);

        if (company is null)
            throw new CompanyNotFoundException(companyId);

        var employeeEntity = _mapper.Map<Employee>(employeeForCreation);

        _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
        await _repository.SaveAsync();

        var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
        return employeeToReturn;
    }

    public async Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid employeeId, bool trackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId);

        var employee = await _repository.Employee.GetEmployeeAsync(companyId, employeeId, trackChanges);
        if (employee is null)
            throw new EmployeeNotFoundException(employeeId);

        _repository.Employee.DeleteEmployee(employee);
        await _repository.SaveAsync();
    }

    public async Task UpdateEmployeeForCompanyAsync(
        Guid companyId,
        Guid employeeId,
        EmployeeForUpdateDto employeeForUpdate,
        bool companyTrackChanges,
        bool employeeTrackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId, companyTrackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId);

        var employee = await _repository.Employee.GetEmployeeAsync(companyId, employeeId, employeeTrackChanges);
        if (employee is null)
            throw new EmployeeNotFoundException(employeeId);

        _mapper.Map(employeeForUpdate, employee);
        await _repository.SaveAsync();
    }

    public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync
        (Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId, compTrackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId);

        var employeeEntity = await _repository.Employee.GetEmployeeAsync(companyId, id,
            empTrackChanges);
        if (employeeEntity is null)
            throw new EmployeeNotFoundException(companyId);

        var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);
        return (employeeToPatch, employeeEntity);
    }

    public async Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)
    {
        _mapper.Map(employeeToPatch, employeeEntity);
        await _repository.SaveAsync();
    }
}