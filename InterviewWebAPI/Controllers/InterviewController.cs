using System.Linq.Expressions;
using InterviewWebAPI.Context.Model;
using InterviewWebAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace InterviewWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/[controller]/[action]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class InterviewController(ILogger<InterviewController> logger, IVacancyRepository vacancyRepository)
        : ControllerBase
    {
        private readonly ILogger<InterviewController> _logger = logger;

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetAllVacancies()
        {
            return Ok(await vacancyRepository.GetAllAsync());
        }

        [Authorize(Roles = "Admin,Recruter")]
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> GetVacancy([FromRoute] int id)
        {
            return Ok(await vacancyRepository.GetItemAsync(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Vacancy vacancy)
        {
            await vacancyRepository.CreateVacancy(vacancy);
            return Created("Post", vacancy);
        }

        [Authorize(Roles = "Admin,Interviewer")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<Vacancy> patchVacancy)
        {
            await vacancyRepository.PatchVacancyAsync(id, patchVacancy);
            return Ok(patchVacancy);
        }

        [Authorize(Roles = "Admin,CM")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact([FromRoute] int id, [FromBody] Vacancy updatedVacancy)
        {
            await vacancyRepository.UpdateVacancyAsync(id, updatedVacancy);
            return Ok(updatedVacancy);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public void Delete()
        {
        }
    }
}