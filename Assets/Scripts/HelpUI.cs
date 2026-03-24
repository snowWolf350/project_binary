using TMPro;
using UnityEngine;

public class HelpUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _helpInfo;

    private void Start()
    {
        HelpTrigger.onHelpEntered += HelpTrigger_onHelpEntered;
        HelpTrigger.onHelpExit += HelpTrigger_onHelpExit;
        Hide();
    }

    private void HelpTrigger_onHelpEntered(object sender, HelpTrigger.onHelpTriggeredEventArgs e)
    {
        _helpInfo.text = e.helpInfo;
        Show();
    }

    private void HelpTrigger_onHelpExit(object sender, System.EventArgs e)
    {
        Hide();
    }



    void Show()
    {
        gameObject.SetActive(true);
    }
    void Hide()
    {
        gameObject.SetActive(false);
    }
}
