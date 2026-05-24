using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Portfolio
{
    public class ProjectileAuthoring : MonoBehaviour
    {
        [SerializeField]
        private float collisionRadius = 0.2f;
        
        private class Baker : Baker<ProjectileAuthoring>
        {
            public override void Bake(ProjectileAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<ProjectileTag>(entity);
                AddComponent<GameEntityTag>(entity);

                AddComponent(entity, new MoveDirection
                {
                    Value = float3.zero
                });

                AddComponent(entity, new MoveSpeed
                {
                    Value = 0f
                });

                AddComponent(entity, new FlyTime
                {
                    Value = 0f
                });

                AddComponent(entity, new CollisionRadius
                {
                    Value = authoring.collisionRadius
                });
            }
        }
    }
}