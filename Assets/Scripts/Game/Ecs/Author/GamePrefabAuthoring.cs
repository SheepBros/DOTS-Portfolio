using Unity.Entities;
using UnityEngine;

namespace Portfolio
{
    public class GamePrefabAuthoring : MonoBehaviour
    {
        [SerializeField]
        private GameObject playerPrefab;

        [SerializeField]
        private GameObject enemyPrefab;

        [SerializeField]
        private GameObject projectilePrefab;

        [SerializeField]
        private float projectileFireCooldown = 0.1f;

        [SerializeField]
        private float projectileSpeed = 12f;

        [SerializeField]
        private float projectileLifetime = 2f;

        [SerializeField]
        private float enemySpawnInterval = 1f;

        [SerializeField]
        private float enemySpawnRectWidth = 24f;

        [SerializeField]
        private float enemySpawnRectHeight = 16f;

        [SerializeField]
        private float enemySpawnCornerRandomRadius = 2f;

        [SerializeField]
        private uint enemyRandomSeed = 1u;

        [SerializeField]
        private int enemyMaxSpawnCount = 100;

        private class Baker : Baker<GamePrefabAuthoring>
        {
            public override void Bake(GamePrefabAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new GamePrefabData
                {
                    PlayerPrefab = GetEntity(authoring.playerPrefab, TransformUsageFlags.Dynamic),
                    EnemyPrefab = GetEntity(authoring.enemyPrefab, TransformUsageFlags.Dynamic),
                    ProjectilePrefab = GetEntity(authoring.projectilePrefab, TransformUsageFlags.Dynamic),

                    ProjectileFireCooldown = authoring.projectileFireCooldown,
                    ProjectileSpeed = authoring.projectileSpeed,
                    ProjectileLifetime = authoring.projectileLifetime,

                    EnemySpawnInterval = authoring.enemySpawnInterval,
                    EnemySpawnRectWidth = authoring.enemySpawnRectWidth,
                    EnemySpawnRectHeight = authoring.enemySpawnRectHeight,
                    EnemySpawnCornerRandomRadius = authoring.enemySpawnCornerRandomRadius,
                    EnemyRandomSeed = authoring.enemyRandomSeed == 0u
                        ? 1u : authoring.enemyRandomSeed,
                    EnemyMaxSpawnCount = authoring.enemyMaxSpawnCount
                });
            }
        }
    }
}