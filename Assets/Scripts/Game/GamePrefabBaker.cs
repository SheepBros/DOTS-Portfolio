using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Portfolio
{
    public class GamePrefabBaker : MonoBehaviour
    {
        public GameObject PlayerPrefab;
        
        public GameObject EnemyPrefab;
        
        public GameObject ProjectilePrefab;

        class Baker : Baker<GamePrefabBaker>
        {
            public override void Bake(GamePrefabBaker authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new GamePrefabData
                {
                    PlayerPrefab = GetEntity(authoring.PlayerPrefab, TransformUsageFlags.Dynamic),
                    EnemyPrefab = GetEntity(authoring.EnemyPrefab, TransformUsageFlags.Dynamic),
                    ProjectilePrefab = GetEntity(authoring.ProjectilePrefab, TransformUsageFlags.Dynamic)
                });
            }
        }
    }
}