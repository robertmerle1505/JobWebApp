using JobWebApp.Model;
using JobWebApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace JobWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        // GET: api/jobModel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobModel>>> GetJobs()
        {
            return Ok(await _jobService.Get());
        }

        // GET: api/jobModel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobModel>> GetJob(int id)
        {
            return Ok(await _jobService.Get(id));
        }

        // POST: api/jobModel
        [HttpPost]
        public async Task<ActionResult<JobModel>> PostJob(JobModel jobModel)
        {
            await _jobService.Add(jobModel);
            return Ok();
        }

        // PUT: api/jobModel/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(int id, JobModel jobModel)
        {
            await _jobService.Update(jobModel);
            return Ok();
        }

        // DELETE: api/jobModel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            await _jobService.Delete(id);
            return Ok();
        }
    }
}
