using Unity.Entities;

namespace Portfolio
{
    public struct ProjectileSpawnData : IComponentData
    {
        public Entity Prefab;
        
        public float FireCooldown;
        
        public float NextFireTime;

        public float Speed;

        public float Lifetime;
    }
}