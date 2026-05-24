using Unity.Entities;

namespace Portfolio
{
    public struct GamePrefabData : IComponentData
    {
        public Entity PlayerPrefab;
        public Entity EnemyPrefab;
        public Entity ProjectilePrefab;
    }
}