using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Portfolio
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(ProjectileEnemyCollisionSystem))]
    public partial struct PlayerEnemyCollisionSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            if (!SystemAPI.TryGetSingleton(out GameState gameState))
            {
                return;
            }

            if (gameState.IsPaused || gameState.IsGameOver)
            {
                return;
            }

            if (!SystemAPI.TryGetSingletonEntity<PlayerTag>(out Entity playerEntity))
            {
                return;
            }

            LocalTransform playerTransform = SystemAPI.GetComponent<LocalTransform>(playerEntity);
            CollisionRadius playerRadius = SystemAPI.GetComponent<CollisionRadius>(playerEntity);
            
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            EntityCommandBuffer ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var (enemyTransform, enemyRadius, enemyEntity)
                     in SystemAPI.Query<RefRO<LocalTransform>, RefRO<CollisionRadius>>()
                         .WithAll<EnemyTag>()
                         .WithEntityAccess())
            {
                float radiusSum = playerRadius.Value + enemyRadius.ValueRO.Value;
                float radiusSumSq = radiusSum * radiusSum;
                float distanceSq = math.distancesq(
                    playerTransform.Position,
                    enemyTransform.ValueRO.Position);

                if (distanceSq > radiusSumSq)
                {
                    continue;
                }

                ecb.DestroyEntity(enemyEntity);

                gameState.Health -= 1;

                if (gameState.Health <= 0)
                {
                    gameState.Health = 0;
                    gameState.IsGameOver = true;
                    break;
                }
            }

            Entity gameStateEntity = SystemAPI.GetSingletonEntity<GameState>();
            SystemAPI.SetComponent(gameStateEntity, gameState);
        }
    }
}