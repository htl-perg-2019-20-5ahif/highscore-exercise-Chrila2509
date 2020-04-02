using HighScoreAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;
using static HighScoreAPI.Controllers.HighScoreController;

namespace APITests
{
    public class UnitTest1
    {
		DataContext _context;
		HighscoreController _controller;


		public UnitTest1()
		{

			var options = new DbContextOptionsBuilder<DataContext>().
				UseSqlServer("Server=Besitzer-PC;Database=HighScore;Integrated Security=SSPI;")
						  .Options;
			_context = new DataContext(options);
			_controller = new HighscoreController(_context);

		}
		[Fact]
		public async void AddHighscore()
		{
			HighScore highScore = new HighScore();
			highScore.User = "AAA";
			highScore.Score = 120;

			await _controller.PostHighScore(highScore);
			Assert.NotEmpty(_context.HighScore);

			clearTest();
		}

		[Fact]
		public async void AddEleventhScore()
		{
			HighScore highScore;
			for(var i = 0; i <= 10; i++)
			{
				highScore = new HighScore();
				highScore.User = "AA" + i;
				highScore.Score = 1000 - i;
				await _controller.PostHighScore(highScore);
			}

			Assert.Equal(10, _controller.GetHighScores().Count());

			clearTest();
		}

		// helping methods
		private async void clearTest()
		{
			_context.HighScore.RemoveRange(_controller.GetHighScores());
			await _context.SaveChangesAsync();
		}
	}
}
