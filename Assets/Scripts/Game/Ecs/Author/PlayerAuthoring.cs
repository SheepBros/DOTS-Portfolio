using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Portfolio
{
    public class PlayerAuthoring : MonoBehaviour
    {
        [SerializeField]
        private int hp = 5;

        [SerializeField]
        private float moveSpeed = 5f;

        [SerializeField]
        private float collisionRadius = 0.5f;
        
        private class Baker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<PlayerTag>(entity);
                AddComponent<GameEntityTag>(entity);

                AddComponent(entity, new MoveDirection
                {
                    Value = float3.zero
                });

                AddComponent(entity, new MoveSpeed
                {
                    Value = authoring.moveSpeed
                });

                AddComponent(entity, new CollisionRadius
                {
                    Value = authoring.collisionRadius
                });

                AddComponent(entity, new Health()
                {
                    Value = authoring.hp
                });
            }
        }
    }
}