using GGJ2025.UIComponents.Menus;
using Karma;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ2025.UIComponents
{
    public class UIManager : Singleton<UIManager>
    {
        [field: SerializeField] public Canvas Canvas { get; private set; }
        public Image fadeImage;
        public GameObject mainMenu;
        public PlayerJoinMenu playerJoinMenu;
        
        private float initialAlpha;
        private float fadeDuration = .5f;
        protected override void Awake()
        {
            base.Awake();
            initialAlpha = fadeImage.color.a;
        }

        public void FadeIn()
        {
            fadeImage.CrossFadeAlpha(0, fadeDuration, false);
        }
        
        public void FadeOut()
        {
            fadeImage.CrossFadeAlpha(initialAlpha, fadeDuration, false);
        }
        
        public void MainMenuStartButton()
        {
            mainMenu.SetActive(false);
            playerJoinMenu.gameObject.SetActive(true);
            GameManager.Instance.EnableJoining();
        }
        
        public void MainMenuExitButton()
        {
            Application.Quit();
        }
        
        public void PlayerJoinMenuBackButton()
        {
            mainMenu.SetActive(true);
            playerJoinMenu.gameObject.SetActive(false);
            GameManager.Instance.ResetPlayers();
            GameManager.Instance.DisableJoining();
        }

        public void PlayerJoinMenuStartButton()
        {
            playerJoinMenu.gameObject.SetActive(false);
            GameManager.Instance.DisableJoining();
            var playerSelections = playerJoinMenu.GetPlayerSelections();
            GameManager.Instance.StartBrawl(playerSelections, playerJoinMenu.MapSelector.SelectedObject, playerJoinMenu.ModeSelector.SelectedObject);
            FadeIn();
        }
    }
}