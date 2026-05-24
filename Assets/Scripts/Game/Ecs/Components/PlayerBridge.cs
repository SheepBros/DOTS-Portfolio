namespace Portfolio
{
    public class PlayerHealthBridge : EcsSingleton<Health>
    {
        public PlayerHealthBridge(IWorldProvider worldProvider) : base(worldProvider)
        {
        }
    }
}