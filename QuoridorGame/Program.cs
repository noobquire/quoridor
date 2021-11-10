using Microsoft.Extensions.DependencyInjection;
using QuoridorGame.Controller;
using QuoridorGame.Model.Entities;
using QuoridorGame.Model.Interfaces;
using QuoridorGame.Model.Logic;
using QuoridorGame.View.Bot;
using Game = QuoridorGame.Model.Entities.QuoridorGame;

namespace QuoridorGame
{
    internal class Program
    {
        static void Main()
        {
            var serviceCollection = new ServiceCollection();
            var game = new Game();
            serviceCollection.AddSingleton(game)
                             .AddScoped<IGraph<Cell>>(c => game.GameField.Cells);
            ConfigureServices(serviceCollection);
            // TODO: Create BotOutput which plays the game
            //var output = new ConsoleOutput();
            var bot = new Bot();
            var output = new BotOutput(bot, game);
            output.ListenTo(game);
            var input = new ConsoleInput(bot);
            input.StartProcessing(game);
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection
            .AddScoped<IPathFinder<CellField, Cell>, PathFinder<CellField, Cell>>()
            .AddScoped<IWallPlacer, WallPlacer>()
            .AddScoped<IMovementLogic, MovementLogic>()
            .AddScoped<IQuoridorGameFacade, QuoridorGameFacade>();
        }
    }
}
