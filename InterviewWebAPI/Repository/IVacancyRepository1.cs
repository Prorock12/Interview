using InterviewWebAPI.Context.Model;

namespace InterviewWebAPI.Repository
{
    public interface IVacancyRepository1
    {
        Task<List<Vacancy>> GetAllAsync();
    }
}