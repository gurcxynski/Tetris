using System;
using System.Timers;

namespace Tetris.Core;

public class StateMachine
{
    public enum GameState { startMenu, running, paused, waiting }
    public GameState state = GameState.startMenu;
    public int max_score = 0;
    Timer Timer = new()
    {
        Enabled = false
    };

    public void Pause()
    {
        state = GameState.paused;
    }
    public void NewGame()
    {
        Game1.scene = new();
        Game1.scene.Initialize();
        state = GameState.running;
        Game1.self.start.Disable();
        Game1.self.menu.Enable();
    }
    public void UnPause()
    {
        state = GameState.running;
    }
    public void GameEnd()
    {
        state = GameState.waiting;
        if (Game1.scene.score > max_score) max_score = Game1.scene.score;
    }

    public void ToStartMenu()
    {
        Game1.self.start.Enable();
        Game1.self.menu.Disable();
        state = GameState.startMenu;
    }
}
