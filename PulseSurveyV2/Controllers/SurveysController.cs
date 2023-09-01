using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PulseSurveyV2.Models;

namespace PulseSurveyV2.Controllers
{
    using Threenine.Data;
    
    [Route("api/[controller]")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        private readonly IUnitOfWork<UnifiedContext> _uow;

        public SurveysController(IUnitOfWork<UnifiedContext> uow)
        {
            _uow = uow;
        }

        // GET: api/Surveys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Survey>>> GetSurveys()
        { 
          var repository = _uow.GetRepositoryAsync<Survey>();
          
          if (repository == null)
          {
              return NotFound();
          }
            var surveys = await repository.GetListAsync();
            return Ok(surveys.Items);
        }

        // GET: api/Surveys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Survey>> GetSurvey(long id)
        { 
            var repository = _uow.GetRepositoryAsync<Survey>(); 
            if (repository == null)
          {
              return NotFound();
          }
            var survey = await repository.SingleOrDefaultAsync(s => s.SurveyId == id);

            if (survey == null)
            {
                return NotFound();
            }

            return survey;
        }

        // POST: api/Surveys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SurveyDTO>> PostSurvey(SurveyDTO surveyDto)
        {
            var repository = _uow.GetRepositoryAsync<Survey>();

          if (repository == null)
          {
              return Problem("Entity set 'UnifiedContext.Surveys'  is null.");
          }

          var survey = new Survey
          {
              UserId = surveyDto.UserId,
              SurveyTitle = surveyDto.SurveyTitle,
              SurveyQuestion = surveyDto.SurveyQuestion,
              SurveyDate = DateTime.UtcNow
          };
            await repository.InsertAsync(survey);
            await _uow.CommitAsync();

            return CreatedAtAction("GetSurvey", new { id = survey.SurveyId }, survey);
        }
        
    }
}
