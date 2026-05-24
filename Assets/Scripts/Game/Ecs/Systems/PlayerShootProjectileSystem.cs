using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Portfolio
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(PlayerShootDirectionSystem))]
    public partial struct PlayerShootProjectileSystem : ISystem
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

            if (!SystemAPI.TryGetSingleton(out PlayerInputData inputData))
            {
                return;
            }

            if (!inputData.IsFirePressed)
            {
                return;
            }

            var ecbSingleton =
                SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            EntityCommandBuffer ecb =
                ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            double elapsedTime = SystemAPI.Time.ElapsedTime;

            foreach (var (playerTransform, spawnData) in
                     SystemAPI.Query<RefRO<LocalTransform>,
                         RefRW<ProjectileSpawnData>>().WithAll<PlayerTag>())
            {
                if (elapsedTime < spawnData.ValueRO.NextFireTime)
                {
                    continue;
                }

                float3 playerPosition = playerTransform.ValueRO.Position;
                float2 shootDirection = inputData.ShootDirection - playerPosition.xy;

                if (math.lengthsq(shootDirection) <= 0.0001f)
                {
                    continue;
                }

                if (math.lengthsq(shootDirection) > 0.0001f)
                {
                    shootDirection = math.normalize(shootDirection);
                }

                Entity projectile = ecb.Instantiate(spawnData.ValueRO.Prefab);
                float3 spawnPosition = playerPosition + new float3(shootDirection.x, shootDirection.y, 0) * 0.7f;

                ecb.SetComponent(projectile, LocalTransform.FromPositionRotationScale(
                    spawnPosition,
                    quaternion.RotateZ(math.atan2(shootDirection.y, shootDirection.x)),
                    1f));

                ecb.SetComponent(projectile, new MoveDirection()
                {
                    Value = new float3(shootDirection.x, shootDirection.y, 0)
                });

                ecb.SetComponent(projectile, new MoveSpeed()
                {
                    Value = spawnData.ValueRO.Speed
                });

                ecb.SetComponent(projectile, new FlyTime()
                {
                    Value = spawnData.ValueRO.Lifetime
                });

                spawnData.ValueRW.NextFireTime = (float)(elapsedTime + spawnData.ValueRO.FireCooldown);
            }
        }
    }
}