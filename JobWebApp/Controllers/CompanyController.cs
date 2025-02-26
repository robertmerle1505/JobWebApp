using JobWebApp.Data;
using JobWebApp.Model;
using JobWebApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET: api/company
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyModel>>> GetCompanies()
        {
            return Ok(await _companyService.Get());
        }

        // GET: api/company/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyModel>> GetCompany(int id)
        {
            return Ok(await _companyService.Get(id));
        }

        // POST: api/company
        [HttpPost]
        public async Task<ActionResult<CompanyModel>> PostCompany(CompanyModel companyModel)
        {
            await _companyService.Add(companyModel);
            return Ok();
        }

        // PUT: api/company/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, CompanyModel companyModel)
        {
            await _companyService.Update(companyModel);
            return Ok();
        }

        // DELETE: api/company/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            await _companyService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("companieswithjobs")]
        public async Task<ActionResult<CompaniesAndJobsModel>> GetCompaniesWithJobs()
        {
            return Ok(await _companyService.GetCompaniesWithJobs());
        }
    }
}
