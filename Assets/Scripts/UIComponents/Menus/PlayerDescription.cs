using TMPro;
using UnityEngine;

namespace GGJ2025.UIComponents.Menus
{
    public class PlayerDescription : MonoBehaviour
    {
        private PlayerInputHandler _playerInputHandler;
        public TextMeshProUGUI PlayerName;
        public BrawlerSelector BrawlerSelector;
        public TextMeshProUGUI JoinedText;
        public TextMeshProUGUI JoinText;
        public void SetPlayerDescription(PlayerInputHandler playerInputHandler)
        {
            _playerInputHandler = playerInputHandler;
            PlayerName.text = $"Player {_playerInputHandler.PlayerIndex + 1}";
            BrawlerSelector.gameObject.SetActive(true);
            JoinedText.gameObject.SetActive(true);
            JoinText.gameObject.SetActive(false);
        }
    }
}