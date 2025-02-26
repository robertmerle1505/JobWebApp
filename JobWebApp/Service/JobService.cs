using System.ComponentModel;
using JobWebApp.Data;
using JobWebApp.Model;
using JobWebApp.Repository;
using JobWebApp.Validation;
using Microsoft.EntityFrameworkCore;

namespace JobWebApp.Service;

public interface IJobService : IDataService<JobModel>
{
}

public class JobService : IJobService
{
    private readonly IJobRepository _jobRepository;

    public JobService(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task Add(JobModel jobModel)
    {
        validateModel(jobModel);

        _jobRepository.Add(CreateEntity(jobModel));
        await _jobRepository.Save();
    }

    private static void validateModel(JobModel jobModel)
    {
        var validationResult = JobValidator.Validate(jobModel);
        if (!validationResult.IsValid)
        {
            throw new InvalidEnumArgumentException(string.Join(',', validationResult.ErrorMessages));
        }
    }

    public async Task Update(JobModel jobModel)
    {
        validateModel(jobModel);
        var job = await _jobRepository.Get(jobModel.Id);
        ModelToEntity(jobModel, job);
        _jobRepository.Update(CreateEntity(jobModel));
        await _jobRepository.Save();
    }

    public async Task Delete(int id)
    {
        var job = await _jobRepository.Get(id);
        _jobRepository.Delete(job);
        await _jobRepository.Save();
    }

    public async Task<IEnumerable<JobModel>> Get()
    {
        return await _jobRepository.GetQueryable()
            .Select(x => new JobModel()
            {
                Id = x.Id, Title = x.Title, Description = x.Description, CompanyId = x.CompanyId, CompanyName = x.Company.Name
            }).ToListAsync();
    }

    public async Task<JobModel> Get(int id)
    {
        var job = await _jobRepository.Get(id);
        return EntityToModel(job);
    }

    private static JobModel EntityToModel(Job job) =>
        new JobModel
        {
            Id = job.Id,
            Title = job.Title,
            Description = job.Description,
            CompanyId = job.CompanyId
        };

    private static void ModelToEntity(JobModel jobModel, Job job)
    {
        job.Id = jobModel.Id;
        job.Title = jobModel.Title;
        job.Description = jobModel.Description;
    }


    private static Job CreateEntity(JobModel jobModel) =>
        new Job
        {
            Id = jobModel.Id,
            Title = jobModel.Title,
            Description = jobModel.Description,
            CompanyId = jobModel.CompanyId
        };
}
