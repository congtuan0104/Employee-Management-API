namespace Service.Contacts;

public interface IServiceManager
{
    ICompanyService CompanyService { get; }

    IEmployeeService EmployeeService { get; }

    IAuthenticationService AuthenticationService { get; }
}