using System;

public class GameField
{
    public int[,] Field { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    public GameField(int width, int height)
    {
        Width = width;
        Height = height;
        Field = new int[Height, Width];
    }

    public bool CanMove(Tetromino tetromino, int offsetX, int offsetY)
    {
        for (int y = 0; y < tetromino.Height; y++)
        {
            for (int x = 0; x < tetromino.Width; x++)
            {
                if (tetromino.Shape[y, x] == 1)
                {
                    int newX = x + offsetX;
                    int newY = y + offsetY;

                    if (newX < 0 || newX >= Width || newY >= Height)
                        return false;
                    if (newY >= 0 && Field[newY, newX] == 1)
                        return false;
                }
            }
        }
        return true;
    }

    public void PlaceTetromino(Tetromino tetromino, int offsetX, int offsetY)
    {
        for (int y = 0; y < tetromino.Height; y++)
        {
            for (int x = 0; x < tetromino.Width; x++)
            {
                if (tetromino.Shape[y, x] == 1)
                {
                    Field[offsetY + y, offsetX + x] = 1;
                }
            }
        }
    }

    public void ClearLines()
    {
        for (int y = 0; y < Height; y++)
        {
            bool isLineFull = true;
            for (int x = 0; x < Width; x++)
            {
                if (Field[y, x] == 0)
                {
                    isLineFull = false;
                    break;
                }
            }

            if (isLineFull)
            {
                for (int row = y; row > 0; row--)
                {
                    for (int col = 0; col < Width; col++)
                    {
                        Field[row, col] = Field[row - 1, col];
                    }
                }
                for (int col = 0; col < Width; col++)
                {
                    Field[0, col] = 0;
                }
            }
        }
    }

    public bool IsGameOver()
    {
        for (int x = 0; x < Width; x++)
        {
            if (Field[0, x] == 1)
                return true;
        }
        return false;
    }

    public void Draw(Tetromino currentTetromino, int offsetX, int offsetY)
    {
        Console.Clear();
        int[,] displayField = (int[,])Field.Clone();

        for (int y = 0; y < currentTetromino.Height; y++)
        {
            for (int x = 0; x < currentTetromino.Width; x++)
            {
                if (currentTetromino.Shape[y, x] == 1)
                {
                    int displayX = offsetX + x;
                    int displayY = offsetY + y;
                    if (displayY >= 0)
                        displayField[displayY, displayX] = 1;
                }
            }
        }

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Console.Write(displayField[y, x] == 1 ? "1" : "0");
            }
            Console.WriteLine();
        }
    }
}
