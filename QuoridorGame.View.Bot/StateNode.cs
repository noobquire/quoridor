using QuoridorGame.View.Bot.Interfaces;
using Game = QuoridorGame.Model.Entities.QuoridorGame;
using QuoridorGame.Model.Entities;
using QuoridorGame.Model.Exceptions;
using System;
using System.Linq;
using QuoridorGame.Model;
using System.Diagnostics;

namespace QuoridorGame.View.Bot
{
    class StateNode : IStateNode
    {
        private Game game;
        private IStaticEvaluationFunction SEV;
        private bool startnode;

        private int bestActionIdx;
        private bool actionIsMove;


        public StateNode(Game game, IStaticEvaluationFunction SEV,
            bool startnode = true)
        {
            this.game = game;
            this.SEV = SEV;
            this.startnode = startnode;
        }

        public void MakeBestMove(bool verbose = false)
        {
            if (actionIsMove)
            {
                var cell = game.AvailableMoves[bestActionIdx];
                game.Move(cell.X, cell.Y);
                if (verbose) 
                {
                    Console.WriteLine($"Bot moved to cell {cell.X} {cell.Y}");
                }
            }
            else 
            {
                var wall = game.AvailableWalls[bestActionIdx];
                game.SetWall(wall.Type, wall.X, wall.Y);

                if (verbose)
                {
                    Console.WriteLine($"Bot placed wall at {wall.X} {wall.Y}" +
                        $" {wall.Type.ToString()}");
                }
            }
        }
       
        public double Rollout(int NRollouts) 
        {
            double Score = 0;

            if (NRollouts > 0)
            {
                //**negamax optimization**
                //+1 for odd (max search) rollouts
                //-1 for even (min search) rollouts
                double sign = Math.Pow(-1, 1 + NRollouts % 2);

                var moveScores = new double[game.AvailableMoves.Length];
                for (int i = 0; i < game.AvailableMoves.Length; i++)
                {
                    var cell = game.AvailableMoves[i];
                    game.Move(cell.X, cell.Y);
                    if (game.State == GameState.FirstPlayerWin ||
                        game.State == GameState.SecondPlayerWin)
                    {
                        moveScores[i] = double.NegativeInfinity;
                        game.PopTurn();
                        break;
                    }
                    else { 
                        var nextState = new StateNode(game, SEV);
                        moveScores[i] = sign * nextState.Rollout(NRollouts - 1);
                        game.PopTurn();
                    }
                }

                var wallScores = new double[game.AvailableWalls.Length+1];
                //Make wallScores size at lest 1 
                wallScores[game.AvailableWalls.Length] = System.Double.PositiveInfinity;
                for (int i = 0; i < game.AvailableWalls.Length; i++) 
                {
                    var wall = game.AvailableWalls[i];
                    game.SetWall(wall.Type, wall.X, wall.Y);
                    var nextState = new StateNode(game, SEV);
                    wallScores[i] = sign * nextState.Rollout(NRollouts - 1);
                    //wallScores[i] = double.PositiveInfinity;
                    game.PopTurn();
                }
               

                double bestMove = moveScores.Min();
                double bestWall = wallScores.Min();
                

                if (bestMove < bestWall)
                {
                    Score = bestMove;
                    actionIsMove = true;
                    bestActionIdx = Array.IndexOf(moveScores, bestMove);
                }
                else 
                {
                    Score = bestWall;
                    actionIsMove = false;
                    bestActionIdx = Array.IndexOf(wallScores, bestWall);
                }
            }
            else 
            {
                Score = SEV.Eval(game);
            }

            return Score;
        }
    }
}
