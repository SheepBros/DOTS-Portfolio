using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Portfolio
{
    public class GameSceneCleaner : ISceneCleaner
    {
        private readonly GameStateBridge _gameStateBridge;

        public GameSceneCleaner(GameStateBridge gameStateBridge)
        {
            _gameStateBridge = gameStateBridge;
        }
        
        public async UniTask CleanAsync()
        {
            Debug.Log("Cleaning Game Scene");
            if (_gameStateBridge.TryGet(out GameState state))
            {
                state.IsPaused = true;
                state.IsGameOver = true;
                _gameStateBridge.Set(state);
            }

            await UniTask.WaitForSeconds(1f, delayTiming: PlayerLoopTiming.FixedUpdate);
        }
    }
}