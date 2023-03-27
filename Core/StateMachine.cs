using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tetris.Core;

public class StateMachine
{
    public enum GameState { startMenu, pauseMenu, running, controls, paused, waiting,
        scores
    }
    public GameState state = GameState.startMenu;
    public List<int> scores = new();

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
        var score = Game1.scene.score;
        scores.Add(score);
        scores.Sort();
        if (scores.Count > 5) scores.RemoveAt(0);
        scores.Reverse();
        var text = "";
        scores.ForEach(item => text += item.ToString() + ";");
        File.WriteAllText("scores.txt", text);
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

    public void ToScoresScreen()
    {
        state = GameState.scores;
        Game1.menus["start"].Disable();
        Game1.menus["scores"].Enable();
    }
}
