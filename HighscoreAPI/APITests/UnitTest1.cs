using HighScoreAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
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
        public void Test1()
        {

        }
    }
}
