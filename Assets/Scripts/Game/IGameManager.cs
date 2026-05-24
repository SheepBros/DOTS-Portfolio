namespace Portfolio
{
    public interface IGameManager
    {
        void StartGame();
        
        void PauseGame();
        
        void Resume();
        
        void EndAndExitGame();
    }
}