namespace Entities.Exceptions;

public sealed class CompanyCollectionBadRequst : BadRequestException
{
    public CompanyCollectionBadRequst() : base("Company collection sent from a client is null.")
    {
    }
}