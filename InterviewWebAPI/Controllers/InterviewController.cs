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
    public class InterviewController(IVacancyRepository vacancyRepository) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetAllVacancies()
        {
            return Ok(await vacancyRepository.GetAllAsync());
        }

        [Authorize(Roles = "Admin,Recruter")]
        [Route("{id:int}")]
        [HttpGet]
        public async Task<ActionResult> GetVacancy([FromRoute] int id)
        {
            return Ok(await vacancyRepository.GetItemAsync(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Vacancy vacancy)
        {
            await vacancyRepository.CreateAsync(vacancy);
            return Created("Post", vacancy);
        }

        [Authorize(Roles = "Admin,Interviewer")]
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<Vacancy> patchVacancy)
        {
            await vacancyRepository.PatchAsync(id, patchVacancy);
            return Ok(patchVacancy);
        }

        [Authorize(Roles = "Admin,CM")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateContact([FromRoute] int id, [FromBody] Vacancy updatedVacancy)
        {
            await vacancyRepository.UpdateAsync(id, updatedVacancy);
            return Ok(updatedVacancy);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await vacancyRepository.DeleteAsync(id);
            return result ? Ok("Deleted") : NoContent();
        }

        [Authorize(Roles = "Admin,Recruter")]
        [HttpGet]
        public ActionResult SearchVacancy(string description)
        {
            return Ok(vacancyRepository.SearchByDescription(description));
        }
    }
}