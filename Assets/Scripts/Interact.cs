using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public string _interactText;

    [SerializeField] TextMeshProUGUI interactText;
    [SerializeField] GameObject interactUI;
    [SerializeField] Sprite _eButtonSprite;
    [SerializeField] Image _eButtonImage;
    public bool _usesInteraction;

    public bool lookAtPlayer;
    [Header("On Interaction")]
    public UnityEvent onInteraction;
    [Header("On Enter")]
    public UnityEvent OnEnter;

    private void Start()
    {
        interactText.text = _interactText;
        if (_usesInteraction)
        {
            _eButtonImage.sprite = _eButtonSprite;
            _eButtonImage.enabled = true;
        }
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
        if(_usesInteraction)
        onInteraction.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        lookAtPlayer = true;
        if(!_usesInteraction)
        OnEnter.Invoke();
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
