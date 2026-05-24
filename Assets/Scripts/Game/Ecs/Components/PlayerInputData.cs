using Unity.Entities;
using Unity.Mathematics;

namespace Portfolio
{
    public struct PlayerInputData : IComponentData
    {
        public float2 Move;

        public float2 ShootDirection;

        public bool IsFirePressed;
    }
}