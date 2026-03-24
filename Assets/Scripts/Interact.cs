using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    public string _interactText;

    [SerializeField] TextMeshProUGUI interactText;
    [SerializeField] GameObject interactUI;

    public UnityEvent onInteraction;

    private void Start()
    {
        Hide();
    }
    public void Interacttion()
    {
        Debug.Log("Interacted");
        onInteraction.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        Show();
        Player.Instance.SetCurrentInteractable(this);
    }
    private void OnTriggerExit(Collider other)
    {
        Hide(); 
        Player.Instance.SetCurrentInteractable(null);
    }

    public void Show()
    {
        interactText.text = _interactText;
        interactUI.SetActive(true);
    }
    public void Hide()
    {
        interactUI.SetActive(false);
    }
}
