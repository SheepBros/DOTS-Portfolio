using System;
using Unity.Entities;

namespace Portfolio
{
    public class EcsEntityBridge<T> : IDisposable where T : unmanaged, IComponentData
    {
        private EntityManager _entityManager;
        
        private EntityQuery _entityQuery;

        public EcsEntityBridge(IWorldProvider worldProvider)
        {
            _entityManager = worldProvider.EntityManager;
            _entityQuery = _entityManager.CreateEntityQuery(ComponentType.ReadWrite<T>());
        }
        
        public bool TryGet(out T value)
        {
            value = _entityQuery.GetSingleton<T>();
            return true;
        }

        public void Set(T value)
        {
            if (TryGet(out T createdValue))
            {
                Entity entity = _entityQuery.GetSingletonEntity();
                _entityManager.SetComponentData(entity, value);
            }
            else
            {
                Entity entity = _entityManager.CreateEntity();
                _entityManager.SetComponentData(entity, value);
            }
        }

        public void Dispose()
        {
            if (!_entityQuery.IsEmpty)
            {
                _entityQuery.Dispose();
            }
        }
    }
}