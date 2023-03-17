using System;

namespace Tetris.Core;

public class StateMachine
{
    public enum GameState { startMenu, optionsMenu, running, paused }
    public GameState state = GameState.startMenu;
    public void Pause()
    {
        state = GameState.paused;
    }
    public void NewGame()
    {
        state = GameState.running;
        Game1.self.start.Disable();
    }
    public void UnPause()
    {
        state = GameState.running;
    }
    public void GameEnd()
    {

    }
    public void OpenOptions()
    {
        Game1.self.start.Disable();
        Game1.self.options.Enable();
        state = GameState.optionsMenu;
    }

    public void ToStartMenu()
    {
        Game1.self.start.Enable();
        Game1.self.options.Disable();
        state = GameState.startMenu;
    }
}
