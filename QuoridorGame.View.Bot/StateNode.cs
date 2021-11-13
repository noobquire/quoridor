using QuoridorGame.View.Bot.Interfaces;
using Game = QuoridorGame.Model.Entities.QuoridorGame;
using QuoridorGame.Model.Entities;
using QuoridorGame.Model.Exceptions;
using System;
using System.Linq;
namespace QuoridorGame.View.Bot
{
    class StateNode : IStateNode
    {
        private Game game;
        private IStaticEvaluationFunction SEV;
        private int actionSpaceDim;
        private int actionNum;
        //public StateNode[] nextStates;
        public double[] nextScores;
        public double SEVScore;
        public int bestMove;

        public StateNode(Game game, IStaticEvaluationFunction SEV, 
            int actionSpaceDim, int actionNum = -1) 
        {
            this.game = game;
            this.SEV = SEV;
            this.actionSpaceDim = actionSpaceDim;
            this.actionNum = actionNum;//-1 for STRT_NODE
            
        }
        private bool MakeActionIFLegal(int actionNum) 
        {
            //make action if allowed

            if (actionNum == -1)
            {
                //pass 
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
        }

        private void RollbackAction(int actionNum) 
        {
            //rollback action
        }

        public double Rollout(int NRollouts)
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
                }
                else if (NRollouts == 0)
                {
                    SEVScore = SEV.Eval(game);
                }

                this.RollbackAction(actionNum);
            }
            return SEVScore;
        }
    }
}
