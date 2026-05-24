namespace Portfolio
{
    public class PlayerInputDataBridge : EcsSingleton<PlayerInputData>
    {
        public PlayerInputDataBridge(IWorldProvider worldProvider) : base(worldProvider)
        {
        }
    }
}