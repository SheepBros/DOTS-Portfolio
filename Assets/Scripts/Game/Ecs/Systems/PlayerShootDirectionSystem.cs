using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Portfolio
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial struct PlayerShootDirectionSystem : ISystem
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

            if (!SystemAPI.TryGetSingleton(out PlayerInputData input))
            {
                return;
            }

            PlayerMoveDirectionJob job = new PlayerMoveDirectionJob()
            {
                PlayerInputData = input
            };
            
            state.Dependency = job.ScheduleParallel(state.Dependency);
        }
    }

    [BurstCompile]
    [WithAll(typeof(PlayerTag))]
    public partial struct PlayerShootDirectionJob : IJobEntity
    {
        public PlayerInputData InputData;
        
        public void Execute(ref LocalTransform transform)
        {
            float2 playerPosition = transform.Position.xy;
            float2 direction = InputData.ShootDirection - playerPosition;

            if (math.lengthsq(direction) <= 0.0001f)
            {
                return;
            }

            float angle = math.atan2(direction.y, direction.x);
            transform.Rotation = quaternion.RotateZ(angle);
        }
    }
}