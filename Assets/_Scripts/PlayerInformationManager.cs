using UnityEngine;
using UnityEngine.UI;

public class PlayerInformationManager : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    [SerializeField] Button[] itemButtons;
    [SerializeField] GameObject[] panels;

    private void Start()
    {
        buttons[0].onClick.AddListener(() => SwitchToPanel(panels[0]));
        buttons[1].onClick.AddListener(() => SwitchToPanel(panels[1]));
        buttons[2].onClick.AddListener(() => SwitchToPanel(panels[2]));
        buttons[3].onClick.AddListener(() => SwitchToPanel(panels[3]));
    }

    void SwitchToPanel(GameObject panelToActive)
    {
        foreach(GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        panelToActive.SetActive(true);
    }
}
