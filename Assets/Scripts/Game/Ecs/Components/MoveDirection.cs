using Unity.Entities;
using Unity.Mathematics;

namespace Portfolio
{
    public struct MoveDirection : IComponentData
    {
        public float3 Value;
    }
}