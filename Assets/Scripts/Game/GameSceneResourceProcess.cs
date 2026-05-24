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
        
        public UniTask LoadAsync(ISceneLoadRequest request)
        {
            Debug.Log("Loading Game Scene Resources...");
            
            _gameManager.StartGame();
            
            return UniTask.CompletedTask;
        }
    }
}