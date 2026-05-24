using Cysharp.Threading.Tasks;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using VContainer.Unity;

namespace Portfolio
{
    public class GameManager : IGameManager, ITickable
    {
        private GameStateBridge _gameStateBridge;
        
        private PlayerInputDataBridge _playerInputDataBridge;
        
        private ISceneTransition _sceneTransition;
        
        private IWorldProvider _worldProvider;

        private EntityQuery _gamePrefabQuery;

        private EntityQuery _gameEntityQuery;

        public GameManager(GameStateBridge gameStateBridge, PlayerInputDataBridge playerInputDataBridge,
            ISceneTransition sceneTransition, IWorldProvider worldProvider)
        {
            _gameStateBridge = gameStateBridge;
            _playerInputDataBridge = playerInputDataBridge;
            _sceneTransition = sceneTransition;
            _worldProvider = worldProvider;
            
            _gamePrefabQuery = _worldProvider.EntityManager.CreateEntityQuery(
                ComponentType.ReadOnly<GamePrefabData>());
            _gameEntityQuery = _worldProvider.EntityManager.CreateEntityQuery(
                ComponentType.ReadOnly<GameEntityTag>());
        }
        
        public async UniTask StartGame()
        {
            ClearGameEntities();

            _gameStateBridge.EnsureExists(new GameState()
            {
                Score = 0,
                IsPaused = false,
                IsGameOver = false
            });

            _playerInputDataBridge.EnsureExists(new PlayerInputData());

            if (_gamePrefabQuery.TryGetSingleton(out GamePrefabData prefabData))
            {
                SpawnPlayer(prefabData);
                CreateEnemySpawner(prefabData);
            }
            else
            {
                Debug.LogError("[GameManager] GamePrefabData를 찾지 못했습니다. GamePrefabAuthoring이 SubScene에 있는지 확인하세요.");
            }
        }

        public void PauseGame()
        {
            if (_gameStateBridge.TryGet(out GameState state))
            {
                state.IsPaused = true;
                _gameStateBridge.Set(state);
            }
        }

        public void Resume()
        {
            if (_gameStateBridge.TryGet(out GameState state))
            {
                state.IsPaused = false;
                _gameStateBridge.Set(state);
            }
        }

        public void EndAndExitGame()
        {
            ClearGameEntities();
            
            _sceneTransition.LoadScene(new TitleSceneLoadRequest(), new GameSceneCleaner()).Forget();
        }

        public void Tick()
        {
            if (!_gameStateBridge.TryGet(out GameState state))
            {
                return;
            }

            if (state.IsGameOver)
            {
                EndAndExitGame();
            }
        }

        private void SpawnPlayer(GamePrefabData prefabData)
        {
            if (prefabData.PlayerPrefab == Entity.Null)
            {
                Debug.LogError("[GameManager] PlayerPrefab이 비어 있습니다.");
                return;
            }

            EntityManager entityManager = _worldProvider.EntityManager;
            Entity player = entityManager.Instantiate(prefabData.PlayerPrefab);

            entityManager.SetComponentData(
                player,
                LocalTransform.FromPosition(float3.zero));

            entityManager.AddComponentData(player, new ProjectileSpawnData
            {
                Prefab = prefabData.ProjectilePrefab,
                FireCooldown = prefabData.ProjectileFireCooldown,
                NextFireTime = 0f,
                Speed = prefabData.ProjectileSpeed,
                Lifetime = prefabData.ProjectileLifetime
            });
        }

        private void CreateEnemySpawner(GamePrefabData prefabData)
        {
            if (prefabData.EnemyPrefab == Entity.Null)
            {
                Debug.LogError("[GameManager] EnemyPrefab이 비어 있습니다.");
                return;
            }

            EntityManager entityManager = _worldProvider.EntityManager;
            Entity spawner = entityManager.CreateEntity();

            entityManager.AddComponentData(spawner, new GameEntityTag());
            entityManager.AddComponentData(spawner, new EnemySpawnData()
            {
                Prefab = prefabData.EnemyPrefab,
                SpawnInterval = prefabData.EnemySpawnInterval,
                NextSpawnTime = 0f,
                RectWidth = prefabData.EnemySpawnRectWidth,
                RectHeight = prefabData.EnemySpawnRectHeight,
                CornerRandomRadius = prefabData.EnemySpawnCornerRandomRadius,
                RandomSeed = prefabData.EnemyRandomSeed,
                SpawnIndex = 0u,
                MaxSpawnCount = prefabData.EnemyMaxSpawnCount
            });
        }
        
        public bool IsReadyToStart()
        {
            EnsureQueries();
            
            return !_gamePrefabQuery.IsEmpty;
        }

        private void EnsureQueries()
        {
            if (_gamePrefabQuery.IsEmpty && _gameEntityQuery.IsEmpty)

            {
                return;
            }

            EntityManager entityManager = _worldProvider.EntityManager;

            if (!_gamePrefabQuery.IsEmpty)
            {
                _gamePrefabQuery = entityManager.CreateEntityQuery(
                    ComponentType.ReadOnly<GamePrefabData>());
            }

            if (!_gameEntityQuery.IsEmpty)
            {
                _gameEntityQuery = entityManager.CreateEntityQuery(
                    ComponentType.ReadOnly<GameEntityTag>());
            }
        }

        private void ClearGameEntities()
        {
            if (!_gameEntityQuery.IsEmpty)
            {
                _worldProvider.EntityManager.DestroyEntity(_gameEntityQuery);
            }
        }
    }
}