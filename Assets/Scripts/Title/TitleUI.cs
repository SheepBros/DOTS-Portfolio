using System;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Portfolio
{
    public class TitleUI : MonoBehaviour
    {
        [SerializeField]
        private Button _playButton;
        
        [SerializeField]
        private Button _quitButton;

        private IApplicationController _applicationController;
        
        private ISceneTransition _sceneTransition;

        [Inject]
        private void Bind(IApplicationController applicationController, ISceneTransition sceneTransition)
        {
            _applicationController = applicationController;
            _sceneTransition = sceneTransition;
        }

        private void Awake()
        {
            _playButton.onClick.AddListener(OnClickedPlayButton);
            _quitButton.onClick.AddListener(OnClickedQuitButton);
        }

        private void OnClickedPlayButton()
        {
            _sceneTransition.LoadScene(new GameSceneLoadRequest(), new TitleSceneCleaner());
        }

        private void OnClickedQuitButton()
        {
            if (_applicationController.IsQuitting)
            {
                return;
            }
            
            _applicationController.Quit();
        }
    }
}