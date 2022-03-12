using DevJobs.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevJobs.API.Persistance.Repositories
{
    public class JobVacancyRepository : IJobVacancyRepositories
    {
        private readonly DevJobContext _context;
        public JobVacancyRepository(DevJobContext context)
        {
            _context = context;
        }
        public void Add(JobVacancy jobVacancy)
        {
            _context.JobVacancies.Add(jobVacancy);

            //Obrigatório para salvar realmente no banco
            _context.SaveChanges();
        }

        public void AddApplication(JobApplication jobApplication)
        {
            _context.JobApplications.Add(jobApplication);
            _context.SaveChanges();
        }

        public List<JobVacancy> GetAll()
        {
            return _context.JobVacancies.ToList();;
        }

        public JobVacancy GetById(int id)
        {
            return _context.JobVacancies
            .Include(jv => jv.Applications)
            .SingleOrDefault(jv => jv.Id == id);
        }

        public void Update(JobVacancy jobVacancy)
        {
            _context.JobVacancies.Update(jobVacancy);
            _context.SaveChanges();
        }
    }
}