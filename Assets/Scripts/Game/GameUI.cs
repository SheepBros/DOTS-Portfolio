using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Portfolio
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _playerHpText;

        [SerializeField]
        private TextMeshProUGUI _scoreText;

        [SerializeField]
        private Button _pauseButton;
        
        [SerializeField]
        private PopupGamePause _popupGamePause;

        [Inject]
        public void Bind(IGameManager gameManager)
        {
            _popupGamePause.Bind(gameManager);
        }

        private void Awake()
        {
            _pauseButton.onClick.AddListener(OnClickedPauseButton);
            _popupGamePause.Hide();
        }

        public void SetPlayerHp(int value)
        {
            _playerHpText.text = $"Hp: {value}";
        }

        public void SetScore(int value)
        {
            _scoreText.text = $"Score: {value}";
        }

        private void OnClickedPauseButton()
        {
            _popupGamePause.Show();
        }
    }
}