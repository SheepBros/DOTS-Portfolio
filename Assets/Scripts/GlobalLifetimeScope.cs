using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Portfolio
{
    public class GlobalLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private LoadingView _loadingView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IApplicationController, ApplicationController>(Lifetime.Singleton);
            builder.Register<ISceneTransition, SceneTransition>(Lifetime.Singleton);
            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
            builder.Register<ISceneResourceLoader, SceneResourceLoader>(Lifetime.Singleton);

            builder.RegisterComponent<ILoadingView>(_loadingView);
        }
    }
}