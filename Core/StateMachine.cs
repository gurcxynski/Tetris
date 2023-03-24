using System;
using System.Timers;

namespace Tetris.Core;

public class StateMachine
{
    public enum GameState { startMenu, pauseMenu, running, controls, paused, waiting }
    public GameState state = GameState.startMenu;
    public int max_score = 0;

    public void Pause()
    {
        state = GameState.paused;
    }
    public void NewGame()
    {
        Game1.scene = new();
        Game1.scene.Initialize();
        state = GameState.running;
        Game1.menus["start"].Disable();
        Game1.menus["ingame"].Enable();
    }
    public void Resume()
    {
        state = GameState.running;
        Game1.menus["pause"].Disable();
        Game1.menus["ingame"].Enable();
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
        Game1.menus["start"].Enable();
        Game1.menus["ingame"].Disable();
        state = GameState.startMenu;
    }
    public void ToPauseMenu()
    {
        if (state == GameState.waiting) { ToStartMenu(); return; }
        Game1.menus["pause"].Enable();
        Game1.menus["ingame"].Disable();
        state = GameState.pauseMenu;
    }

    public void ToControlsScreen()
    {
        state = GameState.controls;
        Game1.menus["pause"].Disable();
        Game1.menus["controls"].Enable();
    }
}
