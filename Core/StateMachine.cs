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

    }
}
