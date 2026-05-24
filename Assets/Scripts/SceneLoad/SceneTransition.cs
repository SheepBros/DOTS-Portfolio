using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Portfolio
{
    public class SceneTransition : ISceneTransition
    {
        private ISceneLoader _sceneLoader;
        
        private ILoadingView _loadingView;
        
        private ISceneResourceLoader _resourceLoader;
        
        public SceneTransition(ISceneLoader sceneLoader, ILoadingView loadingView,
            ISceneResourceLoader resourceLoader)
        {
            _sceneLoader = sceneLoader;
            _loadingView = loadingView;
            _resourceLoader = resourceLoader;
        }
        
        public async UniTask LoadScene(ISceneLoadRequest request, ISceneCleaner cleaner)
        {
            Debug.LogError("[SceneTransition] LoadScene");
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

                await _resourceLoader.LoadAsync(request);
            
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