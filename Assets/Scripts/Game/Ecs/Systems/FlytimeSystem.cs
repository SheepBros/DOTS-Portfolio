using Unity.Burst;
using Unity.Entities;

namespace Portfolio
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(UnitMoveSystem))]
    public partial struct FlytimeSystem : ISystem
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

            var ecbSingleton =
                SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();

            EntityCommandBuffer.ParallelWriter ecb =
                ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
                    .AsParallelWriter();

            FlytimeJob job = new FlytimeJob()
            {
                DeltaTime = deltaTime,
                Ecb = ecb
            };

            state.Dependency = job.ScheduleParallel(state.Dependency);
        }
    }

    [BurstCompile]
    public partial struct FlytimeJob : IJobEntity
    {
        public float DeltaTime;

        public EntityCommandBuffer.ParallelWriter Ecb;

        public void Execute(
            [ChunkIndexInQuery] int sortKey,
            Entity entity,
            ref FlyTime flyTime)
        {
            flyTime.Value -= DeltaTime;
            
            if (flyTime.Value <= 0f)
            {
                Ecb.DestroyEntity(sortKey, entity);
            }
        }
    }
}