using QuoridorGame.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game = QuoridorGame.Model.Entities.QuoridorGame;

namespace QuoridorGame.View.ConsoleGraphics
{
    public class GameViewModel
    {
        public ViewCell[][] ViewField { get; set; }

        public GameViewModel(Game game)
        {
            var viewFieldSize = CellField.FieldSize + WallsGrid.GridSize;
            ViewField = new ViewCell[viewFieldSize][];

            // init empty
            for (int i = 0; i < viewFieldSize; i++)
            {
                ViewField[i] = new ViewCell[viewFieldSize];
                for (int j = 0; j < viewFieldSize; j++)
                {
                    if (i % 2 == 0 && j % 2 == 0)
                    {
                        ViewField[i][j] = new ViewCell(ViewCellType.EmptyCell);
                    }
                    else if (i % 2 != 0 && j % 2 == 0)
                    {
                        ViewField[i][j] = new ViewCell(ViewCellType.NoWallUnderCell);
                    }
                    else
                    {
                        ViewField[i][j] = new ViewCell(ViewCellType.NoWall);
                    }
                }
            }

            // fill player cells
            var firstPlayerCell = game.FirstPlayer.CurrentCell;
            var secondPlayerCell = game.SecondPlayer.CurrentCell;

            ViewField[firstPlayerCell.X * 2][firstPlayerCell.Y * 2].Type = ViewCellType.FirstPlayer;
            ViewField[secondPlayerCell.X * 2][secondPlayerCell.Y * 2].Type = ViewCellType.SecondPlayer;

            // highlight available moves
            var availableMoves = game.AvailableMoves;
            foreach (var cell in availableMoves)
            {
                ViewField[cell.X * 2][cell.Y * 2].Type = ViewCellType.AvailableMoveCell;
            }

            // fill walls
            for (int viewI = 1, wallI = 0; wallI < WallsGrid.GridSize; viewI += 2, wallI++)
            {
                for (int viewJ = 1, wallJ = 0; wallJ < WallsGrid.GridSize; viewJ += 2, wallJ++)
                {
                    var currentWall = game.GameField.Walls[wallI, wallJ];
                    var currentViewCell = ViewField[viewI][viewJ];

                    switch (currentWall.Type)
                    {
                        case WallType.Vertical:
                            var viewCellAbove = ViewField[viewI - 1][viewJ];
                            var viewCellBelow = ViewField[viewI + 1][viewJ];
                            currentViewCell.Type = ViewCellType.Wall;
                            viewCellAbove.Type = ViewCellType.Wall;
                            viewCellBelow.Type = ViewCellType.Wall;
                            break;
                        case WallType.Horizontal:
                            var viewCellLeft = ViewField[viewI][viewJ - 1];
                            var viewCellRight = ViewField[viewI][viewJ + 1];
                            currentViewCell.Type = ViewCellType.Wall;
                            viewCellLeft.Type = ViewCellType.WallUnderCell;
                            viewCellRight.Type = ViewCellType.WallUnderCell;
                            break;
                    }
                }
            }
        }

        public void Print()
        {
            for (int i = 0; i < ViewField.GetLength(0); i++)
            {
                var line = ViewField[i];
                PrintLine(line);
                if (i % 2 == 0)
                {
                    PrintLine(line);
                }
            }
        }

        private void PrintLine(ViewCell[] line)
        {
            foreach (var cell in line)
            {
                WriteColor(cell.Display, cell.Color);
            }
            Console.WriteLine();
        }

        private void WriteColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
        }
    }
}
