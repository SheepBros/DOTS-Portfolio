using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Portfolio
{
    public class GameManager : IGameManager, ITickable
    {
        private GameStateBridge _gameStateBridge;
        
        private PlayerInputDataBridge _playerInputDataBridge;
        
        private SceneTransition _sceneTransition;

        public GameManager(GameStateBridge gameStateBridge, PlayerInputDataBridge playerInputDataBridge,
            SceneTransition sceneTransition)
        {
            _gameStateBridge = gameStateBridge;
            _playerInputDataBridge = playerInputDataBridge;
            _sceneTransition = sceneTransition;
        }
        
        public void StartGame()
        {
            _gameStateBridge.Set(new GameState()
            {
                Health = 5,
                Score = 0,
                IsPaused = false,
                IsGameOver = false
            });

            _playerInputDataBridge.Set(new PlayerInputData());
        }

        public void PauseGame()
        {
            if (_gameStateBridge.TryGet(out GameState state))
            {
                state.IsPaused = true;
                _gameStateBridge.Set(state);
            }
        }

        public void Resume()
        {
            if (_gameStateBridge.TryGet(out GameState state))
            {
                state.IsPaused = false;
                _gameStateBridge.Set(state);
            }
        }

        public void EndAndExitGame()
        {
            _sceneTransition.LoadScene(new TitleSceneLoadRequest(), new GameSceneCleaner(_gameStateBridge)).Forget();
        }

        public void Tick()
        {
            if (!_gameStateBridge.TryGet(out GameState state))
            {
                return;
            }

            if (state.IsGameOver)
            {
                EndAndExitGame();
            }
        }
    }
}