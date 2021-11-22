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
        private double[] nextScores;
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
                    Debug.WriteLine($"Bot moved to cell {cell.X} {cell.Y}");
                }
            }
            else 
            {
                var wall = game.AvailableWalls[bestActionIdx];
                game.SetWall(wall.X, wall.Y, wall.Type);

                if (verbose)
                {
                    Debug.WriteLine($"Bot placed wall at {wall.X} {wall.Y}");
                }
            }
        }
        /*private bool MakeActionIFLegal(int actionNum, bool verbose = false) 
        {
            //make action if allowed

            if (actionNum == -1)
            {
                if (verbose) 
                {
                    Console.WriteLine("Pass");
                }
            }
            else if (actionNum < 12)
            {
                var X = game.CurrentPlayer.CurrentCell.X;
                var Y = game.CurrentPlayer.CurrentCell.Y;
                switch (actionNum)
                {
                    case 0: //UP
                        Y -= 1;
                        break;
                    case 1: //DN
                        Y += 1;
                        break;
                    case 2: //LT
                        X -= 1;
                        break;
                    case 3: //RT
                        X += 1;
                        break;
                    case 4: //UPUP
                        Y -= 2;
                        break;
                    case 5: //DNDN
                        Y += 2;
                        break;
                    case 6: //LTLT
                        X -= 2;
                        break;
                    case 7: //RTRT
                        X += 2;
                        break;
                    case 8: //UPLT
                        Y -= 1;
                        X -= 1;
                        break;
                    case 9: //UPRT
                        Y -= 1;
                        X += 1;
                        break;
                    case 10: //DNLT
                        Y += 1;
                        X -= 1;
                        break;
                    case 11: //DNRT
                        Y += 1;
                        X += 1;
                        break;
                }
                try
                {
                    game.Move(X, Y);
                    if (verbose)
                    { 
                        Console.WriteLine($"Move to {X}, {Y}");
                    }
                    
                }
                catch (QuoridorGameException)
                {
                    return false;
                }
            }
            else if (actionNum < 140)
            {
                WallType walltype = (actionNum < 64) ? WallType.Horizontal : WallType.Vertical;
                try
                {
                    game.SetWall(walltype, actionNum / 8, actionNum % 8);
                    if (verbose)
                    {
                        var wall = walltype is WallType.Horizontal ? "horizonta" : "vertical";
                        Console.WriteLine($"Place {wall} wall to {actionNum / 8}, {actionNum % 8}");
                    }
                }
                catch (QuoridorGameException)
                {
                    return false;
                }
            }
            else
            {
                throw new QuoridorGameException("Invalid move");
            }

            return true;
        }*/
        /*public double Rollout(int NRollouts)
        {
            //NRollouts should be odd to start with max search

            SEVScore = System.Double.NaN;

            if (this.MakeActionIFLegal(actionNum))
            {
                if (NRollouts > 0)
                {
                    //**negamax optimization**
                    //+1 for odd (max search) rollouts
                    //-1 for even (min search) rollouts
                    double sign = Math.Pow(-1, 1 + NRollouts % 2); 
                    nextScores = new double[actionSpaceDim];
                    for (int i = 0; i < actionSpaceDim; i++)
                    {
                        var nextState = new StateNode(game, SEV, actionSpaceDim, i);
                        nextScores[i] = sign * nextState.Rollout(NRollouts - 1);
                    }
                    double maxScore = nextScores.Max();
                    bestMove = Array.IndexOf(nextScores, maxScore);
                    SEVScore = maxScore;
                }
                else if (NRollouts == 0)
                {
                    SEVScore = SEV.Eval(game);
                }

            }
            return SEVScore;
        }*/


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
                    var nextState = new StateNode(game, SEV);
                    moveScores[i] = sign * nextState.Rollout(NRollouts - 1);
                    game.PopTurn();
                }

                var wallScores = new double[game.AvailableWalls.Length+1];
                //Make wallScores size at lest 1 
                wallScores[game.AvailableWalls.Length] = System.Double.NegativeInfinity;
                for (int i = 0; i < game.AvailableWalls.Length; i++) 
                {
                    var wall = game.AvailableWalls[i];
                    game.SetWall(wall.X, wall.Y, wall.Type);
                    var nextState = new StateNode(game, SEV);
                    wallScores[i] = sign * nextState.Rollout(NRollouts - 1);
                    game.PopTurn();
                }
               

                double bestMove = moveScores.Max();
                double bestWall = wallScores.Max();

                if (bestMove > bestWall)
                {
                    Score = bestMove;
                    actionIsMove = true;
                    bestActionIdx = Array.IndexOf(moveScores, bestMove);
                }
                else 
                {
                    Score = bestWall;
                    actionIsMove = true;
                    bestActionIdx = Array.IndexOf(moveScores, bestMove);
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
