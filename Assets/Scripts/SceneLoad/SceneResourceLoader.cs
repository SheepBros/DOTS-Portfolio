using Cysharp.Threading.Tasks;
using VContainer;

namespace Portfolio
{
    public class SceneResourceLoader : ISceneResourceLoader
    {
        private IObjectResolver _resolver;

        public void SetContainer(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public async UniTask LoadAsync(ISceneLoadRequest request)
        {
            await UniTask.WaitUntil(HasSetResolver);
            
            ISceneResourceProcess process = _resolver.Resolve<ISceneResourceProcess>();
            if (process != null)
            {
                await process.LoadAsync(request);
            }

            _resolver = null;
        }
        
        private bool HasSetResolver()
        {
            return _resolver != null;
        }
    }
}