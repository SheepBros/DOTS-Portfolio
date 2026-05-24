using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Portfolio
{
    public class SceneTransition : ISceneTransition
    {
        private ISceneLoader _sceneLoader;
        
        private ILoadingView _loadingView;
        
        public SceneTransition(ISceneLoader sceneLoader, ILoadingView loadingView)
        {
            _sceneLoader = sceneLoader;
            _loadingView = loadingView;
        }
        
        public async UniTask LoadScene(ISceneLoadRequest request, ISceneCleaner cleaner)
        {
            if (request == null)
            {
                Debug.LogError("[SceneTransition] Request is null");
                return;
            }
            
            if (string.IsNullOrEmpty(request.SceneName))
            {
                Debug.LogError("[SceneTransition] Scene name is null or empty.");
                return;
            }

            try
            {
                if (_loadingView != null)
                {
                    await _loadingView.Show();
                }

                if (cleaner != null)
                {
                    await cleaner.CleanAsync();
                }
            
                await _sceneLoader.LoadScene(request.SceneName);

                var resourceLoader = request.CreateResourceLoader();
                if (resourceLoader != null)
                {
                    await resourceLoader.Load();
                }
            
                if (_loadingView != null)
                {
                    await _loadingView.Hide();
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}