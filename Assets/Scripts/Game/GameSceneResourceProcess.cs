using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Portfolio
{
    public class GameSceneResourceProcess : ISceneResourceProcess
    {
        private IGameManager _gameManager;

        [Inject]
        private void Bind(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }
        
        public async UniTask LoadAsync(ISceneLoadRequest request)
        {
            Debug.Log("Loading Game Scene Resources...");

            await UniTask.WaitUntil(_gameManager.IsReadyToStart);
            
            await _gameManager.StartGame();
        }
    }
}