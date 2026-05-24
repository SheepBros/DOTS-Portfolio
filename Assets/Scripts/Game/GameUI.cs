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

        private void Awake()
        {
            _pauseButton.onClick.AddListener(OnClickedPauseButton);
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