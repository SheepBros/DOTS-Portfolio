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

        [Inject]
        private void Bind(IGameManager gameManager)
        {
            _gameManager = gameManager;
            
            Hide();
        }

        private void Awake()
        {
            _resumeButton.onClick.AddListener(OnClickedResumeButton);
            _exitButton.onClick.AddListener(OnClickedExitButton);
        }

        public void Show()
        {
            _root.SetActive(true);
        }

        public void Hide()
        {
            _root.SetActive(false);
        }

        private void OnClickedResumeButton()
        {
            Hide();
            
            _gameManager.Resume();
        }

        private void OnClickedExitButton()
        {
            _gameManager.EndAndExitGame();
        }
    }
}