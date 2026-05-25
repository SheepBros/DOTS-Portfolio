using Unity.Entities;

namespace Portfolio
{
    public class WorldProvider : IWorldProvider
    {
        public World World => World.DefaultGameObjectInjectionWorld;
        
        public EntityManager EntityManager => World.EntityManager;
        
        public bool IsWorldAlive => World != null && World.IsCreated;
    }
}