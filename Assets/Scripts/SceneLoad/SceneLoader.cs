using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Portfolio
{
    public class SceneLoader : ISceneLoader
    {
        private readonly SceneLoadEvent _sceneLoadEvent;

        public bool IsLoading { get; private set; }

        public SceneLoader(SceneLoadEvent sceneLoadEvent)
        {
            _sceneLoadEvent = sceneLoadEvent;
        }

        public async UniTask LoadScene(string sceneName)
        {
            if (IsLoading)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(sceneName))
            {
                Debug.LogError("[SceneLoader] Scene name is null or empty.");
                return;
            }

            IsLoading = true;
            
            _sceneLoadEvent.OnPreLoad?.Invoke();

            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            
            _sceneLoadEvent.OnPostLoad?.Invoke();

            IsLoading = false;
        }
    }
}