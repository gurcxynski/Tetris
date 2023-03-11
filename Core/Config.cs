using System.Numerics;

namespace Tetris.Core;


public static class Settings
{
    public static bool music = true;
}
public static class Config
{
    public static int tickSpeed = 300;
    public static int cellsX = 20;
    public static int cellsY = 35;
    public static int cellSize = 20;
    public static Vector2 start = new(cellsX / 2, 3);
}
