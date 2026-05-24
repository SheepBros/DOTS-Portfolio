using VContainer.Unity;

namespace Portfolio
{
    public class GameUIUpdater : ITickable
    {
        private GameUI _gameUI;
        
        private GameStateBridge _gameStateBridge;
        
        private PlayerHealthBridge _playerHealthBridge;

        public GameUIUpdater(GameUI gameUI, GameStateBridge gameStateBridge,
            PlayerHealthBridge playerHealthBridge)
        {
            _gameUI = gameUI;
            _gameStateBridge = gameStateBridge;
            _playerHealthBridge = playerHealthBridge;
        }
        
        public void Tick()
        {
            if (_gameStateBridge.TryGet(out GameState state))
            {
                _gameUI.SetScore(state.Score);
            }
            
            if (_playerHealthBridge.TryGet(out Health hp))
            {
                _gameUI.SetPlayerHp(hp.Value);
            }
        }
    }
}