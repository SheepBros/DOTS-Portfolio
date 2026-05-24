using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Portfolio
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(UnitMoveSystem))]
    public partial struct ProjectileEnemyCollisionSystem : ISystem
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

            NativeList<Entity> projectileEntities = new NativeList<Entity>(Allocator.Temp);
            NativeList<float3> projectilePositions = new NativeList<float3>(Allocator.Temp);
            NativeList<float> projectileRadius = new NativeList<float>(Allocator.Temp);

            foreach (var (transform, radius, entity)
                     in SystemAPI.Query<RefRO<LocalTransform>, RefRO<CollisionRadius>>()
                         .WithAll<ProjectileTag>()
                         .WithEntityAccess())
            {
                projectileEntities.Add(entity);
                projectilePositions.Add(transform.ValueRO.Position);
                projectileRadius.Add(radius.ValueRO.Value);
            }

            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            EntityCommandBuffer ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (enemyTransform, enemyRadius, enemyEntity)
                     in SystemAPI.Query<RefRO<LocalTransform>, RefRO<CollisionRadius>>()
                         .WithAll<EnemyTag>()
                         .WithEntityAccess())
            {
                for (int i = 0; i < projectileEntities.Length; i++)
                {
                    Entity projectileEntity = projectileEntities[i];
                    if (!state.EntityManager.Exists(projectileEntity))
                    {
                        continue;
                    }

                    float radiusSum = enemyRadius.ValueRO.Value + projectileRadius[i];
                    float radiusSumSq = radiusSum * radiusSum;
                    float distanceSq = math.distancesq(
                        enemyTransform.ValueRO.Position,
                        projectilePositions[i]);

                    if (distanceSq > radiusSumSq)
                    {
                        continue;
                    }

                    ecb.DestroyEntity(projectileEntity);
                    ecb.DestroyEntity(enemyEntity);

                    gameState.Score += 9;
                    break;
                }
            }

            Entity gameStateEntity = SystemAPI.GetSingletonEntity<GameState>();
            SystemAPI.SetComponent(gameStateEntity, gameState);

            projectileEntities.Dispose();
            projectilePositions.Dispose();
            projectileRadius.Dispose();
        }
    }
}