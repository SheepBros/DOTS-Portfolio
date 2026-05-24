using VContainer.Unity;

namespace Portfolio
{
    public class GameUIUpdater : ITickable
    {
        private GameUI _gameUI;
        
        private GameStateBridge _gameStateBridge;

        public GameUIUpdater(GameUI gameUI, GameStateBridge gameStateBridge)
        {
            _gameUI = gameUI;
            _gameStateBridge = gameStateBridge;
        }
        
        public void Tick()
        {
            if (!_gameStateBridge.TryGet(out GameState state))
            {
                return;
            }
            
            _gameUI.SetPlayerHp(state.Health);
            _gameUI.SetScore(state.Score);
        }
    }
}