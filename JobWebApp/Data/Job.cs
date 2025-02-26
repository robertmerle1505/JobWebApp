using System.ComponentModel.DataAnnotations.Schema;

namespace JobWebApp.Data;

public class Job
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public int CompanyId { get; set; }
    public Company Company { get; set; }
}

