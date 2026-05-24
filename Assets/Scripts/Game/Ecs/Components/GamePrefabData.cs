using Unity.Entities;

namespace Portfolio
{
    public struct GamePrefabData : IComponentData
    {
        public Entity PlayerPrefab;
        public Entity EnemyPrefab;
        public Entity ProjectilePrefab;

        public float ProjectileFireCooldown;
        public float ProjectileSpeed;
        public float ProjectileLifetime;

        public float EnemySpawnInterval;
        public float EnemySpawnRectWidth;
        public float EnemySpawnRectHeight;
        public float EnemySpawnCornerRandomRadius;
        public uint EnemyRandomSeed;
        public int EnemyMaxSpawnCount;
    }
}