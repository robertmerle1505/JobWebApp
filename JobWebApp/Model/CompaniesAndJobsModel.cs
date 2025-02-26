namespace JobWebApp.Model;

public class CompaniesAndJobsModel
{
    public CompanyModel company{ get; set; }
    public IEnumerable<JobModel> Jobs { get; set; }
}