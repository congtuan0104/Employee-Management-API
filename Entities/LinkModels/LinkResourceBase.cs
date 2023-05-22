namespace Entities.LinkModels;

public class LinkResourceBase
{
    public LinkResourceBase()
    {
        Console.WriteLine("LinkResourceBase constructor");
    }

    public List<Link> Links { get; set; } = new();
}