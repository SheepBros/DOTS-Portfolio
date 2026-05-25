using System;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Portfolio
{
    public class PopupGamePause : MonoBehaviour, IPopup
    {
        [SerializeField]
        private GameObject _root;
        
        [SerializeField]
        private Button _resumeButton;
        
        [SerializeField]
        private Button _exitButton;
        
        private IGameManager _gameManager;

        public void Bind(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void Awake()
        {
            _resumeButton.onClick.AddListener(OnClickedResumeButton);
            _exitButton.onClick.AddListener(OnClickedExitButton);
        }

        public void Show()
        {
            _gameManager.PauseGame();
            
            _root.SetActive(true);
        }

        public void Hide()
        {
            _root.SetActive(false);
            
            _gameManager.Resume();
        }

        private void OnClickedResumeButton()
        {
            Hide();
        }

        private void OnClickedExitButton()
        {
            _gameManager.EndAndExitGame();
        }
    }
}