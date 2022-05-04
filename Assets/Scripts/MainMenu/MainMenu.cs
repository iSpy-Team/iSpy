using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject panelMenu, panelSetting, panelChangeName;

        [SerializeField] private Slider bg;
        [SerializeField] private Slider sfx;

        private void Start()
        {
            SetMenu(true, false, false);
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
            SetMenu(false, true, false);
        }

        public void Back()
        {
            SetMenu(true, false, false);
        }

        private void SetMenu(bool menuActive, bool settingActive, bool changeName)
        {
            panelMenu.SetActive(menuActive);
            panelSetting.SetActive(settingActive);
            panelChangeName.SetActive(changeName);
        }
        
        public void ChangeName()
        {
            SetMenu(false,false,true);
        }
        
    }
}