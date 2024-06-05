using System.Data.Entity.Core;
using InterviewWebAPI.Context;
using InterviewWebAPI.Context.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace InterviewWebAPI.Repository
{
    public interface IVacancyRepository
    {
        public Task<List<Vacancy>> GetAllAsync();
        public Task<Vacancy> GetItemAsync(int id);
        public Task CreateVacancy(Vacancy vacancy);
        public Task UpdateVacancyAsync(int id, Vacancy newVacancy);
        public Task PatchVacancyAsync(int id, JsonPatchDocument<Vacancy> patchVacancy);
    }

    public class VacancyRepository(InterviewDbContext dbContext) : IVacancyRepository
    {
        public async Task CreateVacancy(Vacancy vacancy)
        {
            await dbContext.Vacancies.AddAsync(vacancy);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Vacancy>> GetAllAsync()
        {
            return await dbContext.Vacancies.ToListAsync();
        }

        public async Task<Vacancy> GetItemAsync(int id)
        {
            return await dbContext.Vacancies.FindAsync(id) ?? throw new ObjectNotFoundException();
        }

        public async Task UpdateVacancyAsync(int id, Vacancy newVacancy)
        {
            await dbContext.Vacancies.Where(x => x.VacancyId == id)
                    .ExecuteUpdateAsync(x => x
                        .SetProperty(y => y.Title, newVacancy.Title)
                        .SetProperty(y => y.Description, newVacancy.Description)
                        .SetProperty(y => y.ProposalCompensation, newVacancy.ProposalCompensation));
        }

        public async Task PatchVacancyAsync(int id, JsonPatchDocument<Vacancy> patchVacancy)
        {
            var vacancy = await dbContext.Vacancies.FindAsync(id);
            if (vacancy != null)
            {
               var s = patchVacancy.Operations[0];
                // await dbContext.Vacancies.Where(x => x.VacancyId == id)
                //     .ExecuteUpdateAsync(x => x
                //         .SetProperty(y => y.Title, newVacancy.Title)
                //         .SetProperty(y => y.Description, newVacancy.Description)
                //         .SetProperty(y => y.ProposalCompensation, newVacancy.ProposalCompensation));
            }
        }
    }
}