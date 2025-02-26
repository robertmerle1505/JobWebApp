using System.ComponentModel.DataAnnotations.Schema;

namespace JobWebApp.Data;

public class Company
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    public ICollection<Job> Jobs { get; set; }
}