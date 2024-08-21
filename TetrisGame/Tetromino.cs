public class Tetromino
{
    public int[,] Shape { get; private set; }
    public int Width => Shape.GetLength(1);
    public int Height => Shape.GetLength(0);

    public Tetromino(int[,] shape)
    {
        Shape = shape;
    }

    public void Rotate()
    {
        int height = Shape.GetLength(0);
        int width = Shape.GetLength(1);
        int[,] rotatedShape = new int[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                rotatedShape[x, height - 1 - y] = Shape[y, x];
            }
        }

        Shape = rotatedShape;
    }
}
