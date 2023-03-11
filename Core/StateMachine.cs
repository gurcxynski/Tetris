namespace Tetris.Core;

public class StateMachine
{
    public enum GameState { startMenu, optionsMenu, running, drawingText }
    public GameState state = GameState.startMenu;
    public void Pause()
    {

    }
    public void NewGame()
    {
        state = GameState.running;
        Game1.self.start.Disable();
    }
    public void UnPause()
    {

    }
    public void GameEnd()
    {

    }
    public void OpenOptions()
    {

    }
}
