using Unity.Entities;

namespace Portfolio
{
    public interface IWorldProvider
    {
        World World { get; }
        
        EntityManager EntityManager { get; }
    }
}