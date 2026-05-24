using Unity.Entities;

namespace Portfolio
{
    public class GameStateBridge : EcsSingleton<GameState>
    {
        public GameStateBridge(IWorldProvider worldProvider) : base(worldProvider)
        {
        }
    }
}