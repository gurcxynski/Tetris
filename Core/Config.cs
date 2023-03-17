using System.Numerics;

namespace Tetris.Core;

public static class Settings
{
    public static bool music = true;
}
public static class Config
{
    public static Vector2 margin = new(100, 50);
    public static Vector2 queuePosition = new(11, 3);
    public static Vector2 heldPosition = new(-2, 3);
    public static int tickSpeed = 300;
    public static int cellsX = 10;
    public static int cellsY = 20;
    public const int cellSize = 30;
    public static Vector2 start = new(cellsX / 2, 1);
}
