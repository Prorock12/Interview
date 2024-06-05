namespace InterviewWebAPI.Context.Model
{
    public class Vacancy
    {
        public int? VacancyId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal? ProposalCompensation { get; set; }
    }
}
