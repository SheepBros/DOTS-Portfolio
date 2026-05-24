using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Portfolio
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial struct UnitMoveSystem : ISystem
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
            
            float deltaTime = SystemAPI.Time.DeltaTime;

            UnitMoveJob job = new UnitMoveJob()
            {
                DeltaTime = deltaTime
            };

            state.Dependency = job.ScheduleParallel(state.Dependency);
        }
    }
    
    [BurstCompile]
    public partial struct UnitMoveJob : IJobEntity
    {
        public float DeltaTime;

        public void Execute(
            ref LocalTransform transform,
            in MoveSpeed speed,
            in MoveDirection direction)
        {
            transform.Position += direction.Value * speed.Value * DeltaTime;
        }
    }
}