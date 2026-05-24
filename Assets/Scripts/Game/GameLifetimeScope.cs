using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Portfolio
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private GameUI _gameUI;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameManager>().As<IGameManager>();
            builder.Register<GameSceneResourceProcess>(Lifetime.Singleton)
                .As<ISceneResourceProcess>();
            builder.Register<PlayerInput>(Lifetime.Singleton);

            builder.Register<IWorldProvider, WorldProvider>(Lifetime.Singleton);
            builder.Register<GameStateBridge>(Lifetime.Singleton);
            builder.Register<PlayerInputDataBridge>(Lifetime.Singleton);
            builder.Register<PlayerHealthBridge>(Lifetime.Singleton);

            builder.RegisterComponent(_gameUI);
            builder.RegisterEntryPoint<GameUIUpdater>();
            
            builder.RegisterBuildCallback(container =>
            {
                ISceneResourceLoader sceneResourceLoader =
                    container.Resolve<ISceneResourceLoader>();
                sceneResourceLoader.SetContainer(container);
            });
        }
    }
}