using Unity.Entities;

namespace Portfolio
{
    public class GameStateBridge : EcsEntityBridge<GameState>
    {
        public GameStateBridge(IWorldProvider worldProvider) : base(worldProvider)
        {
        }
    }
}