using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Portfolio
{
    public class EnemyAuthoring : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed = 2.5f;
        
        [SerializeField]
        private float collisionRadius = 0.5f;

        private class Baker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<EnemyTag>(entity);
                AddComponent<GameEntityTag>(entity);

                AddComponent(entity, new MoveSpeed
                {
                    Value = authoring.moveSpeed
                });

                AddComponent(entity, new MoveDirection
                {
                    Value = float3.zero
                });

                AddComponent(entity, new CollisionRadius
                {
                    Value = authoring.collisionRadius
                });
            }
        }
    }
}