using Cysharp.Threading.Tasks;

namespace Portfolio
{
    public interface IGameManager
    {
        UniTask StartGame();
        
        void PauseGame();
        
        void Resume();
        
        void EndAndExitGame();

        bool IsReadyToStart();

        void ClearGameEntities();
    }
}