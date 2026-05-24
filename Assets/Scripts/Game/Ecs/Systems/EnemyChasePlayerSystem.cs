using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Portfolio
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(EnemySpawnSystem))]
    public partial struct EnemyChasePlayerSystem : ISystem
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

            LocalTransform playerTransform =
                SystemAPI.GetComponent<LocalTransform>(playerEntity);
            float3 playerPosition = playerTransform.Position;
            EnemyMoveJob job = new EnemyMoveJob()
            {
                PlayerPosition = playerPosition
            };

            state.Dependency = job.ScheduleParallel(state.Dependency);
        }
    }

    [BurstCompile]
    [WithAll(typeof(EnemyTag))]
    public partial struct EnemyMoveJob : IJobEntity
    {
        public float3 PlayerPosition;
        
        public void Execute(ref MoveDirection moveDirection,
            in LocalTransform enemyTransform)
        {
            float3 playerDirection = PlayerPosition - enemyTransform.Position;
            playerDirection.z = 0;

            if (math.lengthsq(playerDirection) > 0.0001f)
            {
                moveDirection.Value = math.normalize(playerDirection);
            }
            else
            {
                moveDirection.Value = float3.zero;
            }
        }
    }
}