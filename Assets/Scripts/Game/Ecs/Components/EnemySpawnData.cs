using Unity.Entities;

namespace Portfolio
{
    public struct EnemySpawnData : IComponentData
    {
        public Entity Prefab;
        
        public float SpawnInterval;
        
        public float NextSpawnTime;

        public float RectWidth;
        
        public float RectHeight;
        
        public float CornerRandomRadius;

        public uint RandomSeed;

        public uint SpawnIndex;
        
        public int MaxSpawnCount;
    }
}