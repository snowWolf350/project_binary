using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    public string _interactText;

    [SerializeField] TextMeshProUGUI interactText;
    [SerializeField] GameObject interactUI;

    bool lookAtPlayer;
    public UnityEvent onInteraction;

    private void Start()
    {
        Hide();
    }
    private void LateUpdate()
    {
        if (lookAtPlayer == false) return;
        Vector3 lookDirection = transform.position - Camera.main.transform.position;
        interactUI.transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
    }
    public void Interacttion()
    {
        onInteraction.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        lookAtPlayer = true;
        Show();
        Player.Instance.SetCurrentInteractable(this);
    }
    private void OnTriggerExit(Collider other)
    {
        lookAtPlayer = false;
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
