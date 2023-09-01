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
    using Threenine.Data;
    
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IUnitOfWork<UnifiedContext> _uow;

        public AnswersController(IUnitOfWork<UnifiedContext> uow)
        {
            _uow = uow;
        }
        
        // GET: api/Answers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswers()
        {
            var repository = _uow.GetRepositoryAsync<Answer>();
          if (repository == null)
          {
              return NotFound();
          }
            var answers = await repository.GetListAsync();

            return Ok(answers.Items);
        }

        // GET: api/Answers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Answer>> GetAnswer(long id)
        {
            var repository = _uow.GetRepositoryAsync<Answer>();
          if (repository == null)
          {
              return NotFound();
          }
            var answer = await repository.SingleOrDefaultAsync(a => a.AnswerId == id);

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
            var repository = _uow.GetRepositoryAsync<Answer>();
          if (repository == null)
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

            await repository.InsertAsync(answer);
            await _uow.CommitAsync();

            return CreatedAtAction(
                "GetAnswer", 
                new { id = answer.AnswerId}, answer);
        }
    }
}
