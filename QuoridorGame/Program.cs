using Microsoft.Extensions.DependencyInjection;
using QuoridorGame.Model.Interfaces;
using QuoridorGame.Model.Logic;
using Game = QuoridorGame.Model.Entities.QuoridorGame;
using QuoridorGame.Model.Entities;

namespace QuoridorGame
{
    internal class Program
    {
        static void Main()
        {
            var game = new Game();
            var services = new ServiceCollection();
            services.AddSingleton(game);
            services.AddScoped<IGraph<Cell>>(c => game.GameField.Cells);
            services.AddScoped<IPathFinder<CellField, Cell>, PathFinder<CellField, Cell>>();
            var serviceProvider = services.BuildServiceProvider();
        }
    }
}
