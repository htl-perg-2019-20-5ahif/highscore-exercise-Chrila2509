using HighScoreAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HighScoreAPI.Controllers
{
    public class HighScoreController
    {
        [Route("api/[controller]")]
        [ApiController]
        public class HighscoreController : ControllerBase
        {
            private readonly DataContext _context;

            public HighscoreController(DataContext context)
            {
                _context = context;
            }

            // get all HighScores
            [HttpGet]
            public IEnumerable<HighScore> GetHighScores()
            {
                return _context.HighScore.OrderByDescending(h => h.Score).ToList();
            }

            // get single HighScore with id
            [HttpGet("{id}")]
            public async Task<ActionResult<HighScore>> GetHighScore(Guid id)
            {
                var highScore = await _context.HighScore.FindAsync(id);

                if (highScore == null)
                {
                    return NotFound();
                }

                return highScore;
            }

            // post HighScore
            [HttpPost]
            public async Task<ActionResult<HighScore>> PostHighScore(HighScore highScore)
            {
                if (_context.HighScore.Count() >= 10)
                {
                    var lastHighScore = _context.HighScore.OrderByDescending(h => h.Score).Last();
                    if (lastHighScore.Score < highScore.Score)
                    {
                        _context.HighScore.Remove(lastHighScore);
                    }
                    else
                    {
                        return BadRequest("This score is not a highscore");
                    }
                }

                _context.HighScore.Add(highScore);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetHighScore", new { id = highScore.HighScoreId }, highScore);
            }

            // delete HighScore with id
            [HttpDelete("{id}")]
            public async Task<ActionResult<HighScore>> DeleteHighScore(Guid id)
            {
                var highScore = await _context.HighScore.FindAsync(id);
                if (highScore == null)
                {
                    return NotFound();
                }

                _context.HighScore.Remove(highScore);
                await _context.SaveChangesAsync();

                return highScore;
            }

            private bool HighScoreExists(Guid id)
            {
                return _context.HighScore.Any(e => e.HighScoreId == id);
            }
        }
    }
}
