using Service.Contacts;

namespace Service;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<ICompanyService> _companyService;
    private readonly Lazy<IEmployeeService> _employeeService;
    
    public ServiceManager(ICompanyService companyService, IEmployeeService employeeService)
    {
        _companyService = new Lazy<ICompanyService>(() => companyService);
        _employeeService = new Lazy<IEmployeeService>(() => employeeService);
    }
    
    public ICompanyService CompanyService => _companyService.Value;
    
    public IEmployeeService EmployeeService => _employeeService.Value;
}