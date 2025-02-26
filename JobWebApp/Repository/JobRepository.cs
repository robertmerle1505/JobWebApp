using JobWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace JobWebApp.Repository;

public interface IJobRepository : IRepository<Job>
{
    IQueryable<Job> GetQueryable();
}

public class JobRepository : IJobRepository
{
    private readonly AppDbContext _dbContext;

    public JobRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;

    }
    public void Add(Job job)
    {
        _dbContext.Add(job);
    }

    public void Update(Job job)
    {
        _dbContext.Entry(job).State = EntityState.Modified;
    }

    public void Delete(Job job)
    {
        _dbContext.Jobs.Remove(job);
    }

    public async Task<IEnumerable<Job>> Get()
    {
        return await _dbContext.Jobs.ToListAsync();
    }

    public async Task<Job> Get(int id)
    {
        var job = await _dbContext.Jobs.FindAsync(id);

        if (job == null)
        {
            throw new KeyNotFoundException();
        }

        return job;
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public IQueryable<Job> GetQueryable()
    {
        return _dbContext.Jobs.AsQueryable();
    }
}
