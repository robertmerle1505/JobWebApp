namespace JobWebApp.Model;

public class JobModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public int CompanyId { get; set; }

    public string? CompanyName { get; set; }
}
