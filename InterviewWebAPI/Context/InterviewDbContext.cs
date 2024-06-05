using InterviewWebAPI.Context.Model;
using Microsoft.EntityFrameworkCore;

namespace InterviewWebAPI.Context
{
    public class InterviewDbContext : DbContext
    {
        public InterviewDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Vacancy> Vacancies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vacancy>().HasData(
                new Vacancy() { VacancyId = 1, Description = "John", Title = "Developer", ProposalCompensation = 30000 },
                new Vacancy() { VacancyId = 2, Description = "Chris", Title = "Manager", ProposalCompensation = 50000 },
                new Vacancy() { VacancyId = 3, Description = "Mukesh", Title = "Consultant", ProposalCompensation = 20000 });
        }
    }
}
