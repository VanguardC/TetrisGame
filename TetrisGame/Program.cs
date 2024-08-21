using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        do
        {
            PlayGame();
        }
        while (AskToPlayAgain());
    }

    static void PlayGame()
    {
        GameField gameField = new GameField(10, 20);
        Tetromino currentTetromino = CreateRandomTetromino();
        int tetrominoX = gameField.Width / 2 - currentTetromino.Width / 2;
        int tetrominoY = 0;
        DateTime lastUpdate = DateTime.Now;
        TimeSpan fallInterval = TimeSpan.FromMilliseconds(500);
        bool gameOver = false;

        while (!gameOver)
        {
            if (DateTime.Now - lastUpdate > fallInterval)
            {
                lastUpdate = DateTime.Now;

                if (gameField.CanMove(currentTetromino, tetrominoX, tetrominoY + 1))
                {
                    tetrominoY++;
                }
                else
                {
                    gameField.PlaceTetromino(currentTetromino, tetrominoX, tetrominoY);
                    gameField.ClearLines();
                    currentTetromino = CreateRandomTetromino();
                    tetrominoX = gameField.Width / 2 - currentTetromino.Width / 2;
                    tetrominoY = 0;

                    if (!gameField.CanMove(currentTetromino, tetrominoX, tetrominoY))
                    {
                        gameField.Draw(currentTetromino, tetrominoX, tetrominoY);
                        Console.Clear();
                        Console.WriteLine("Проигрыш!");
                        gameOver = true;
                    }
                }
                gameField.Draw(currentTetromino, tetrominoX, tetrominoY);
            }

            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        if (gameField.CanMove(currentTetromino, tetrominoX - 1, tetrominoY))
                        {
                            tetrominoX--;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (gameField.CanMove(currentTetromino, tetrominoX + 1, tetrominoY))
                        {
                            tetrominoX++;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (gameField.CanMove(currentTetromino, tetrominoX, tetrominoY + 1))
                        {
                            tetrominoY++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        currentTetromino.Rotate();
                        if (!gameField.CanMove(currentTetromino, tetrominoX, tetrominoY))
                        {
                            currentTetromino.Rotate();
                            currentTetromino.Rotate();
                            currentTetromino.Rotate();
                        }
                        break;
                }
                gameField.Draw(currentTetromino, tetrominoX, tetrominoY);
            }
        }
    }

    static Tetromino CreateRandomTetromino()
    {
        int[,] shape = {
            { 1, 1, 1, 1 }
        };
        return new Tetromino(shape);
    }

    static bool AskToPlayAgain()
    {
        Console.WriteLine("Хотите сыграть еще раз? (Y/N)");
        var input = Console.ReadLine();
        return input?.Trim().ToUpper() == "Y";
    }
}
