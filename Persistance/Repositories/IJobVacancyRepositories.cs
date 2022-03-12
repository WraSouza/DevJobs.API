using DevJobs.API.Entities;

namespace DevJobs.API.Persistance.Repositories
{
    public interface IJobVacancyRepositories
    {
         List<JobVacancy> GetAll();

         JobVacancy GetById(int id);
         void Add(JobVacancy jobVacancy);
         void Update(JobVacancy jobVacancy);

         void AddApplication(JobApplication jobApplication);
    }
}