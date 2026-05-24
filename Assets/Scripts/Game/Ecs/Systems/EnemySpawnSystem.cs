using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Portfolio
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(EnemyChasePlayerSystem))]
    public partial struct EnemySpawnSystem : ISystem
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
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            EntityCommandBuffer.ParallelWriter ecb =
                ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();

            var job = new EnemySpawnJob
            {
                ElapsedTime = SystemAPI.Time.ElapsedTime,
                PlayerPosition = playerTransform.Position,
                Ecb = ecb
            };

            state.Dependency = job.ScheduleParallel(state.Dependency);
        }

        [BurstCompile]
        private partial struct EnemySpawnJob : IJobEntity
        {
            public double ElapsedTime;

            public float3 PlayerPosition;

            public EntityCommandBuffer.ParallelWriter Ecb;

            private void Execute(
                [ChunkIndexInQuery] int sortKey,
                ref EnemySpawnData spawnData)
            {
                if (spawnData.Prefab == Entity.Null)
                {
                    return;
                }

                if (spawnData.SpawnIndex >= spawnData.MaxSpawnCount)
                {
                    return;
                }

                if (ElapsedTime < spawnData.NextSpawnTime)
                {
                    return;
                }

                float3 spawnPosition = CalculateSpawnPosition(ref spawnData, PlayerPosition);
                Entity enemy = Ecb.Instantiate(
                    sortKey,
                    spawnData.Prefab);

                Ecb.SetComponent(
                    sortKey,
                    enemy,
                    LocalTransform.FromPosition(spawnPosition));

                spawnData.NextSpawnTime = (float)(ElapsedTime + spawnData.SpawnInterval);
                spawnData.SpawnIndex++;
            }

            private static float3 CalculateSpawnPosition(
                ref EnemySpawnData spawnData,
                float3 playerPosition)
            {
                uint seed = spawnData.RandomSeed;
                if (seed == 0)
                {
                    seed = 1;
                }

                seed += spawnData.SpawnIndex * 747796405u + 2891336453u;

                Random random = Random.CreateFromIndex(seed);
                int cornerIndex = random.NextInt(0, 4);
                float halfWidth = spawnData.RectWidth * 0.5f;
                float halfHeight = spawnData.RectHeight * 0.5f;
                float2 cornerOffset = cornerIndex switch
                {
                    0 => new float2(-halfWidth, -halfHeight),
                    1 => new float2(halfWidth, -halfHeight),
                    2 => new float2(-halfWidth, halfHeight),
                    _ => new float2(halfWidth, halfHeight)
                };

                float randomX = random.NextFloat(
                    -spawnData.CornerRandomRadius,
                    spawnData.CornerRandomRadius);

                float randomY = random.NextFloat(
                    -spawnData.CornerRandomRadius,
                    spawnData.CornerRandomRadius);

                float2 randomOffset = new float2(randomX, randomY);
                float2 spawnPosition2D = playerPosition.xy + cornerOffset + randomOffset;

                return new float3(spawnPosition2D.x, spawnPosition2D.y, playerPosition.z);
            }
        }
    }
}