namespace Entities.Exceptions;

public sealed class CompanyNotFoundException : NotFoundException
{
    public CompanyNotFoundException(Guid companyId)
        : base($"Company with id: {companyId} doesn't exist in the database.")
    {
    }
}