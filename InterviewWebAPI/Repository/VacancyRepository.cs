using System.Data.Entity.Core;
using InterviewWebAPI.Context;
using InterviewWebAPI.Context.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace InterviewWebAPI.Repository
{
    public interface IVacancyRepository
    {
        Task<List<Vacancy>> GetAllAsync();
        Task<Vacancy> GetItemAsync(int id);
        Task CreateAsync(Vacancy vacancy);
        Task UpdateAsync(int id, Vacancy newVacancy);
        Task PatchAsync(int id, JsonPatchDocument<Vacancy> patchVacancy);
        List<Vacancy> SearchByDescription(string description);
        Task<bool> DeleteAsync(int id);
    }

    public class VacancyRepository(InterviewDbContext dbContext) : IVacancyRepository
    {
        public async Task CreateAsync(Vacancy vacancy)
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

        public async Task UpdateAsync(int id, Vacancy newVacancy)
        {
            await dbContext.Vacancies.Where(x => x.VacancyId == id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(y => y.Title, newVacancy.Title)
                    .SetProperty(y => y.Description, newVacancy.Description)
                    .SetProperty(y => y.ProposalCompensation, newVacancy.ProposalCompensation));
        }

        public async Task PatchAsync(int id, JsonPatchDocument<Vacancy> patchVacancy)
        {
            var vacancy = await dbContext.Vacancies.FindAsync(id);
            if (vacancy != null)
            {
                patchVacancy.ApplyTo(vacancy);
                await dbContext.SaveChangesAsync();
            }
        }

        public List<Vacancy> SearchByDescription(string description)
        {
            return dbContext.Vacancies
                .Where(x => x.Description != null && EF.Functions.FreeText(x.Description, description)).ToList();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var record = await dbContext.Vacancies.FindAsync(id);
            if (record == null)
            {
                return false;
            }

            dbContext.Vacancies.Remove(record);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}