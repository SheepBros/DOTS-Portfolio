using System;
using Unity.Entities;
using UnityEngine;

namespace Portfolio
{
    public class EcsSingleton<T> : IDisposable where T : unmanaged, IComponentData
    {
        private EntityManager _entityManager;
        
        private EntityQuery _query;
        
        private bool _isDisposed;

        public EcsSingleton(IWorldProvider worldProvider)
        {
            _entityManager = worldProvider.EntityManager;
            _query = _entityManager.CreateEntityQuery(ComponentType.ReadWrite<T>());
        }

        public bool TryGet(out T value)
        {
            if (_isDisposed)
            {
                value = default;
                return false;
            }

            if (_query.CalculateEntityCount() != 1)
            {
                value = default;
                return false;
            }

            value = _query.GetSingleton<T>();
            return true;
        }

        public void Set(T value)
        {
            if (_isDisposed)
            {
                return;
            }

            if (_query.CalculateEntityCount() != 1)
            {
                Debug.LogError($"[EcsSingleton] {typeof(T).Name} singleton count is not 1.");
                return;
            }

            Entity entity = _query.GetSingletonEntity();
            _entityManager.SetComponentData(entity, value);
        }

        public void EnsureExists(T initialValue)
        {
            if (_isDisposed)
            {
                return;
            }

            int count = _query.CalculateEntityCount();

            if (count == 0)
            {
                Entity entity = _entityManager.CreateEntity(typeof(T));
                _entityManager.SetComponentData(entity, initialValue);
                return;
            }

            if (count == 1)
            {
                Set(initialValue);
                return;
            }

            Debug.LogError($"[EcsSingleton] Multiple {typeof(T).Name} singletons exist.");
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }
            
            _isDisposed = true;
            
            if (!_query.IsEmpty)
            {
                _query.Dispose();
            }
        }
    }
}