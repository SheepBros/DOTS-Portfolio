using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Portfolio
{
    public class TitleLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private TitleUI _titleUI;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_titleUI);
        }
    }
}