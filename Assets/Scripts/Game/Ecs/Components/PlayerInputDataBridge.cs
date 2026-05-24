namespace Portfolio
{
    public class PlayerInputDataBridge : EcsEntityBridge<PlayerInputData>
    {
        public PlayerInputDataBridge(IWorldProvider worldProvider) : base(worldProvider)
        {
        }
    }
}