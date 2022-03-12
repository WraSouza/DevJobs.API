namespace DevJobs.API.Controllers
{
    using DevJobs.API.Entities;
    using DevJobs.API.Models;
    using DevJobs.API.Persistance;
    using DevJobs.API.Persistance.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Serilog;

    [Route("api/job-vacancies")]
    [ApiController]
    public class JobVacanciesController : ControllerBase
    {
        private readonly IJobVacancyRepositories _repository;
        public JobVacanciesController(IJobVacancyRepositories repository)
        {
            _repository = repository;
        }
        // GET api/job-vacancies
        [HttpGet]
        public IActionResult GetAll()
        {
            var jobvacancies = _repository.GetAll();
            return Ok(jobvacancies);
        }

        // GET api/job-vacancies/4
        [HttpGet("{id}")]         
        public IActionResult GetById(int id)
        {
            var jobVacancy = _repository.GetById(id);           

            if(jobVacancy == null)
            return NotFound();

            return Ok(jobVacancy);
        }

        // POST api/job-vacancies
        /// <summary>
        /// Cadastrar uma vaga de emprego.
        /// </summary>
        /// <remarks>
        ///{
        ///"title": "Dev .NET Jr",
        ///"description": "Vaga para sustentação de aplicações .NET Core",
        /// "company": "Nome da Empresa",
        ///"isRemote": true,
        ///"salaryRange": "3000 a 5000"
        ///} 
        /// </remarks>
        /// <param name="model">Dados da Vaga.</param>
        /// <returns>Objeto Recém Criado</returns>
        /// <response code="201">Sucesso.</response>
        /// <response code="400">Dados Inválidos.</response>
        [HttpPost]
        public IActionResult Post(AddJobVacancyInputModel model)
        {
            Log.Information("POST JobVacancy chamado");
           var jobVacancy = new JobVacancy(
               model.Title,
               model.Description,
               model.Company,
               model.IsRemote,
               model.SalaryRange
           );

           //Como exemplo, não é recomendado essa parte
           if(jobVacancy.Title.Length > 30)
           return BadRequest("Título precisa ter menos de 30 caracteres");

            _repository.Add(jobVacancy);

            return CreatedAtAction("GetById", new { id= jobVacancy.Id }, jobVacancy);
        }

        // PUT api/job-vacancies/4
        [HttpPut("{id}")]
        public IActionResult Put(int id,UpdateJobVacancyInputModel model)
        {
            var jobVacancy = _repository.GetById(id);

            if(jobVacancy == null)
            return NotFound();

            jobVacancy.Update(model.Title,model.Description);

            _repository.Update(jobVacancy);

            return NoContent();
        }
    }
}