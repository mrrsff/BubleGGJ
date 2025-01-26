using GGJ2025.UIComponents.Menus;
using Karma;
using Karma.Extensions;
using TMPro;
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
        public GameObject winMenu;
        public TextMeshProUGUI winText;
        
        private BrawlerHUD[] brawlerHUDs;
        
        private float initialAlpha;
        private float fadeDuration = .5f;
        protected override void Awake()
        {
            base.Awake();
            initialAlpha = fadeImage.color.a;
            brawlerHUDs = GetComponentsInChildren<BrawlerHUD>(true);
        }

        public void FadeIn()
        {
            fadeImage.CrossFadeAlpha(0, fadeDuration, true);
        }
        
        public void FadeOut()
        {
            fadeImage.CrossFadeAlpha(initialAlpha, fadeDuration, true);
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

        public void SetBrawlers(Brawler[] brawlers)
        {
            for (int i = 0; i < brawlerHUDs.Length; i++)
            {
                if (i >= brawlers.Length)
                {
                    brawlerHUDs[i].gameObject.SetActive(false);
                }
                else
                {
                    brawlerHUDs[i].gameObject.SetActive(true);
                    brawlerHUDs[i].SetBrawler(brawlers[i]);
                }
            }
        }

        public void ShowWinMenu(Brawler winner)
        {
            FadeOut();
            Time.timeScale = 0;
            winText.text = winner.name.Bold() + "WON!";
            winMenu.SetActive(true);
        }
    }
}