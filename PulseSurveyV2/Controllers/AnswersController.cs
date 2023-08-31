using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PulseSurveyV2.Models;
using PulseSurveyV2.Interfaces;

namespace PulseSurveyV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly UnifiedContext _context;

        public AnswersController(UnifiedContext context)
        {
            _context = context;
        }

        // GET: api/Answers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswers()
        {
          if (_context.Answers == null)
          {
              return NotFound();
          }
            return await _context.Answers.ToListAsync();
        }

        // GET: api/Answers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Answer>> GetAnswer(long id)
        {
          if (_context.Answers == null)
          {
              return NotFound();
          }
            var answer = await _context.Answers.FindAsync(id);

            if (answer == null)
            {
                return NotFound();
            }

            return answer;
        }


        // POST: api/Answers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AnswerDTO>> PostAnswer(AnswerDTO answerDto)
        {
          if (_context.Answers == null)
          {
              return Problem("Entity set 'UnifiedContext.Answers'  is null.");
          }
            var answer = new Answer
            {
                SurveyId = answerDto.SurveyId,
                UserId = answerDto.UserId,
                AnswerRating = answerDto.AnswerRating,
                AnswerDate = DateTime.UtcNow
            };
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                "GetAnswer", 
                new { id = answer.AnswerId}, answer);
        }
        

        private bool AnswerExists(long id)
        {
            return (_context.Answers?.Any(e => e.AnswerId == id)).GetValueOrDefault();
        }
    }
}
