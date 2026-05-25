using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Portfolio
{
    public class GameSceneCleaner : ISceneCleaner
    {
        private readonly IGameManager _gameManager;

        public GameSceneCleaner(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }
        
        public async UniTask CleanAsync()
        {
            _gameManager.ClearGameEntities();
            
            await UniTask.WaitForSeconds(1f, delayTiming: PlayerLoopTiming.FixedUpdate);
        }
    }
}