using AutoMapper;
using Contracts;
using Entities.ConfigurationModels;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Service.Contacts;

namespace Service;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthenticationService> _authenticationService;
    private readonly Lazy<ICompanyService> _companyService;
    private readonly Lazy<IEmployeeService> _employeeService;

    public ServiceManager(IRepositoryManager repositoryManager,
        ILoggerManager logger,
        IMapper mapper,
        IEmployeeLinks employeeLinks,
        UserManager<User> userManager,
        IOptions<JwtConfiguration> configuration)
    {
        _companyService = new Lazy<ICompanyService>(() => new
            CompanyService(repositoryManager, logger, mapper));

        _employeeService = new Lazy<IEmployeeService>(() => new
            EmployeeService(repositoryManager, logger, mapper, employeeLinks));

        _authenticationService = new Lazy<IAuthenticationService>(() => new
            AuthenticationService(logger, mapper, userManager, configuration));
    }

    public IAuthenticationService AuthenticationService => _authenticationService.Value;

    public ICompanyService CompanyService => _companyService.Value;

    public IEmployeeService EmployeeService => _employeeService.Value;
}