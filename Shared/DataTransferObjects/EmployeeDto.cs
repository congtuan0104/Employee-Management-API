namespace Shared.DataTransferObjects;

public record EmployeeDto
{
    public Guid CompanyId { get; init; }

    public string? Name { get; init; }

    public int Age { get; init; }

    public string? Position { get; init; }
}