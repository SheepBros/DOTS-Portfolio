using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace Portfolio
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(UnitMoveSystem))]
    public partial struct PlayerMoveDirectionSystem : ISystem
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

            if (!SystemAPI.TryGetSingleton(out PlayerInputData playerInputData))
            {
                return;
            }
            
            PlayerMoveDirectionJob job = new PlayerMoveDirectionJob()
            {
                PlayerInputData = playerInputData
            };

            state.Dependency = job.ScheduleParallel(state.Dependency);
        }
    }
    
    [BurstCompile]
    [WithAll(typeof(PlayerTag))]
    public partial struct PlayerMoveDirectionJob : IJobEntity
    {
        public PlayerInputData PlayerInputData;
        
        public void Execute(ref MoveDirection moveDirection)
        {
            float3 move = new float3(PlayerInputData.Move.x, PlayerInputData.Move.y, 0);
            if (math.lengthsq(move) > 0.0001f)
            {
                moveDirection.Value = math.normalize(move);
            }
            else
            {
                moveDirection.Value = move;
            }
        }
    }
}