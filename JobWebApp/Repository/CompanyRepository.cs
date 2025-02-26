using JobWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace JobWebApp.Repository;

public interface ICompanyRepository : IRepository<Company>
{
    Task<IEnumerable<Company>> GetCompaniesWithJobs();
}

public class CompanyRepository : ICompanyRepository
{
    private readonly AppDbContext _dbContext;

    public CompanyRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;

    }

    public void Add(Company company)
    {
        _dbContext.Add(company);
    }

    public void Update(Company company)
    {
        _dbContext.Entry(company).State = EntityState.Modified;
    }

    public void Delete(Company company)
    {
        _dbContext.Companies.Remove(company);
    }

    public async Task<IEnumerable<Company>> Get()
    {
        return await _dbContext.Companies.ToListAsync();
    }

    public async Task<Company> Get(int id)
    {
        var company = await _dbContext.Companies.FindAsync(id);

        if (company == null)
        {
            throw new KeyNotFoundException();
        }

        return company;
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Company>> GetCompaniesWithJobs()
    {
        return await _dbContext.Companies
            .Include(c => c.Jobs)
            .Where(c => c.Jobs.Any())
            .ToListAsync();
    }
}
