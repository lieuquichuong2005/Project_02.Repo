using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingInGame : MonoBehaviour
{
    public Button settingPanelButton;
    public Button accountPanelButton;
    public Button helpPanelButton;
    public Button homeButton;
    public Button exitGameButton;
    public Button confirmHomeButton;
    public Button confirmExitGameButton;
    public Button cancelHomeButton;
    public Button cancelExitGameButton;

    public Slider musicSettingSlider;
    public Slider sfxSettingSlider;
    public Toggle fullscreenToggle;
    public TMP_Dropdown languageDropdown;

    public GameObject[] panels;

    void Start()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
            Debug.Log("Đã Tắt Tất Cả Panel");
        }

        settingPanelButton.onClick.AddListener(OnSettingButtonClick);
        accountPanelButton.onClick.AddListener(OnAccountButtonClick);
        helpPanelButton.onClick.AddListener(OnHelpButtonClick);
        homeButton.onClick.AddListener(OnHomeButtonClick);
        exitGameButton.onClick.AddListener(OnExitGameButtonClick);
        confirmHomeButton.onClick.AddListener(OnConfirmHomeButtonClick);
        confirmExitGameButton.onClick.AddListener(OnConfirmExitGameButtonClick);

        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggle);

        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float soundEffectVolume = PlayerPrefs.GetFloat("SoundEffectVolume", 1f);

        AudioManager.instance.SetMusicVolume(musicVolume);
        AudioManager.instance.SetSoundEffectVolume(soundEffectVolume);

        musicSettingSlider.value = musicVolume;
        sfxSettingSlider.value = soundEffectVolume;

        musicSettingSlider.onValueChanged.AddListener(AudioManager.instance.SetMusicVolume);
        sfxSettingSlider.onValueChanged.AddListener(AudioManager.instance.SetSoundEffectVolume);

    }

    void OnSettingButtonClick()
    {
        PanelToActive(panels[0]);
    }   
    void OnAccountButtonClick()
    {
        PanelToActive(panels[1]);
    }   
    void OnHelpButtonClick()
    {
        PanelToActive(panels[2]);
    }
    void OnHomeButtonClick()
    {
        PanelToActive(panels[3]);
    }
    void OnExitGameButtonClick()
    {
        PanelToActive(panels[4]);
    }
    void OnConfirmHomeButtonClick()
    {
        SceneManager.LoadScene("HomeScene");
    }
    void OnConfirmExitGameButtonClick()
    {
        Application.Quit();
    }    
    void PanelToActive(GameObject panelToActive)
    {
        foreach(GameObject panel in panels)
        {
            panel.SetActive(panel ==  panelToActive);
        }    
    }
    public void OnFullscreenToggle(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }
}
