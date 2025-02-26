using JobWebApp.Data;
using JobWebApp.Model;
using JobWebApp.Repository;
using JobWebApp.Validation;
using System.ComponentModel;

namespace JobWebApp.Service;

public interface ICompanyService : IDataService<CompanyModel>
{
    Task<IEnumerable<CompaniesAndJobsModel>> GetCompaniesWithJobs();
}

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task Add(CompanyModel companyModel)
    {
        validateModel(companyModel);
        _companyRepository.Add(CreateEntity(companyModel));
        await _companyRepository.Save();
    }

    public async Task Update(CompanyModel companyModel)
    {
        validateModel(companyModel);
        var company = await _companyRepository.Get(companyModel.Id);
        ModelToEntity(companyModel, company);
        _companyRepository.Update(company);
        await _companyRepository.Save();
    }

    public async Task Delete(int id)
    {
        var company = await _companyRepository.Get(id);
        _companyRepository.Delete(company);
        await _companyRepository.Save();
    }

    public async Task<IEnumerable<CompanyModel>> Get()
    {
        return (await _companyRepository.Get()).Select(x => new CompanyModel() { Id = x.Id, Name = x.Name, Address = x.Address });
    }

    public async Task<CompanyModel> Get(int id)
    {
        var company = await _companyRepository.Get(id);
        return EntityToModel(company);
    }

    public async Task<IEnumerable<CompaniesAndJobsModel>> GetCompaniesWithJobs()
    {
        return (await _companyRepository.GetCompaniesWithJobs()).AsQueryable()
            .Select(c => new CompaniesAndJobsModel()
            {
                company = EntityToModel(c),
                Jobs = c.Jobs.Select(j => new JobModel()
                {
                    Id = j.Id,
                    Title = j.Title,
                    Description = j.Description
                })
            });
    }

    private static CompanyModel EntityToModel(Company company) =>
        new CompanyModel
        {
            Id = company.Id,
            Name = company.Name,
            Address = company.Address
        };
    private static void ModelToEntity(CompanyModel companyModel, Company company)
    {
        company.Id = companyModel.Id;
        company.Name = companyModel.Name;
        company.Address = companyModel.Address;
    }

    private static Company CreateEntity(CompanyModel companyModel) =>
        new Company
        {
            Id = companyModel.Id,
            Name = companyModel.Name,
            Address = companyModel.Address
        };

    private static void validateModel(CompanyModel companyModel)
    {
        var validationResult = CompanyValidator.Validate(companyModel);
        if (!validationResult.IsValid)
        {
            throw new InvalidEnumArgumentException(string.Join(',', validationResult.ErrorMessages));
        }
    }
}
