using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Portfolio
{
    public class SceneLoader : ISceneLoader
    {
        public bool IsLoading { get; private set; }

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

            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            IsLoading = false;
        }
    }
}