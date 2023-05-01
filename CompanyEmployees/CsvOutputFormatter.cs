using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Shared.DataTransferObjects;

namespace CompanyEmployees;

// Add content negotiation for CSV format
public class CsvOutputFormatter : TextOutputFormatter
{
    public CsvOutputFormatter()
    {
        // define which media type this formatter should parse as well as encodings.
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }

    protected override bool CanWriteType(Type? type)
    {
        // indicates whether or not the CompanyDto type can be written by this serializer.
        if (typeof(CompanyDto).IsAssignableFrom(type) ||
            typeof(IEnumerable<CompanyDto>).IsAssignableFrom(type))
            return base.CanWriteType(type);
        return false;
    }

    private static void FormatCsv(StringBuilder buffer, CompanyDto company)
    {
        buffer.AppendLine($"{company.Id},\"{company.Name}\",\"{company.FullAddress}\"");
    }

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext
        context, Encoding selectedEncoding) // constructs the response
    {
        var response = context.HttpContext.Response;
        var buffer = new StringBuilder();

        if (context.Object is IEnumerable<CompanyDto>)
            foreach (var company in (IEnumerable<CompanyDto>)context.Object)
                FormatCsv(buffer, company);
        else
            FormatCsv(buffer, (CompanyDto)context.Object);
        await response.WriteAsync(buffer.ToString());
    }
}