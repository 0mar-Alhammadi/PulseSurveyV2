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
    [Route("api/[controller]")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        private readonly UnifiedContext _context;

        public SurveysController(UnifiedContext context)
        {
            _context = context;
        }

        // GET: api/Surveys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Survey>>> GetSurveys()
        {
          if (_context.Surveys == null)
          {
              return NotFound();
          }
            return await _context.Surveys.ToListAsync();
        }

        // GET: api/Surveys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Survey>> GetSurvey(long id)
        {
          if (_context.Surveys == null)
          {
              return NotFound();
          }
            var survey = await _context.Surveys.FindAsync(id);

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
          if (_context.Surveys == null)
          {
              return Problem("Entity set 'UnifiedContext.Surveys'  is null.");
          }

          var survey = new Survey
          {
              UserId = surveyDto.UserId,
              SurveyTitle = surveyDto.SurveyTitle,
              SurveyQuestion = surveyDto.SurveyQuestion,
              SurveyDate = DateTime.Now
          };
            _context.Surveys.Add(survey);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSurvey", new { id = survey.SurveyId }, survey);
        }
        
        private bool SurveyExists(long id)
        {
            return (_context.Surveys?.Any(e => e.SurveyId == id)).GetValueOrDefault();
        }
    }
}
