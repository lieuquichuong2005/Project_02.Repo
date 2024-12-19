using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public GameObject[] panels;

    void Start()
    {
        settingPanelButton.onClick.AddListener(OnSettingButtonClick);
        accountPanelButton.onClick.AddListener(OnAccountButtonClick);
        helpPanelButton.onClick.AddListener(OnHelpButtonClick);
        homeButton.onClick.AddListener(OnHomeButtonClick);
        exitGameButton.onClick.AddListener(OnExitGameButtonClick);
        confirmHomeButton.onClick.AddListener(OnConfirmHomeButtonClick);
        confirmExitGameButton.onClick.AddListener(OnConfirmExitGameButtonClick);

        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
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
}
