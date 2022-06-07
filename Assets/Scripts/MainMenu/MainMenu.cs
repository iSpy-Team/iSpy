using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

namespace MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject panelMenu, panelSetting, panelChangeName, creditsPanel, tutorialPanel;

        [SerializeField] private GameObject serverBrowserUI;

        [SerializeField] private Slider bg;
        [SerializeField] private Slider sfx;

        // Matchmaker fields

        [SerializeField] private GameObject failedConnect;
        [SerializeField] private string AWSserver;
        private bool connected = false;

        private bool isSBShown = false;

        public RectTransform mainMenu, serverMenu;

        private void Start()
        {
            MatchmakerClient.OnMClientConnected += OnConnectedToMatchmaker;
            MatchmakerClient.OnMClientDisconnected += OnDisconnectedFromMatchmaker;
            MatchmakerClient.OnMCFailedToConnect += OnFailedToConnectToMatchmaker;
            SetMenu(true, false, false, false, false);
            B_HideServerBrowser();
        }

        private void OnFailedToConnectToMatchmaker()
        {
            Instantiate(failedConnect, transform);
        }

        private void OnDisconnectedFromMatchmaker()
        {
            connected = false;
            B_HideServerBrowser();
            Back();
        }

        private void OnConnectedToMatchmaker()
        {
            connected = true;
            mainMenu.DOAnchorPos(new Vector2(-2000, 0), 1.0f);
            serverMenu.DOAnchorPos(new Vector2(0, 0), 1.0f);
            isSBShown = true;
            serverBrowserUI.SetActive(true);
        }

        private void Update()
        {
            SetValue();
        }

        private void SetValue()
        {
            PlayerPrefs.SetFloat(Constant.BACKGROUND_AUDIO, bg.value);
            PlayerPrefs.SetFloat(Constant.SFX_AUDIO, sfx.value);
        }

        public void Play()
        {
            SceneManager.LoadScene("Lobby");
        }

        public void Setting()
        {
            SetMenu(false, true, false, false, false);
        }

        public void Back()
        {
            SetMenu(true, false, false, false, false);
        }

        public void Credits()
        {
            SetMenu(false, false, false, true, false);
        }

        private void SetMenu(bool menuActive, bool settingActive, bool changeName, bool credits, bool tutorial)
        {
            panelMenu.SetActive(menuActive);
            panelSetting.SetActive(settingActive);
            panelChangeName.SetActive(changeName);
            creditsPanel.SetActive(credits);
            tutorialPanel.SetActive(tutorial);
        }

        public void ChangeName()
        {
            SetMenu(false, false, true, false, false);
        }

        public void ShowTutorial()
        {
            SetMenu(false, false, false, false, true);
        }

        public void B_ConnectMatchmaker()
        {
            if (isSBShown) return;
            if (!MatchmakerClient.Singleton.IsConnected) MatchmakerClient.Singleton.Connect(AWSserver, 7777);
            if (connected) OnConnectedToMatchmaker();
        }

        public void B_HideServerBrowser()
        {
            if (!isSBShown) return;
            mainMenu.DOAnchorPos(new Vector2(0, 0), 1.0f);
            serverMenu.DOAnchorPos(new Vector2(2000, 0), 1.0f);
            isSBShown = false;
            serverBrowserUI.SetActive(false);
        }

        private void OnDestroy()
        {
            MatchmakerClient.OnMClientConnected -= OnConnectedToMatchmaker;
            MatchmakerClient.OnMClientDisconnected -= OnDisconnectedFromMatchmaker;
            MatchmakerClient.OnMCFailedToConnect -= OnFailedToConnectToMatchmaker;
        }

        public void QuitApplication()
        {
            Application.Quit(0);
        }
    }
}