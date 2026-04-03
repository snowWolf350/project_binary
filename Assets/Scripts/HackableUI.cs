using UnityEngine;
using UnityEngine.UI;

public class HackableUI : MonoBehaviour
{
    [SerializeField] Image _fillImage;
    [SerializeField] Outline _Outline;

    private void Start()
    {
        GameInput.Instance.onProgressChanged += GameInput_onProgressChanged;
    }

    private void GameInput_onProgressChanged(object sender, IHasProgress.onProgressChangedEventArgs e)
    {
        _fillImage.fillAmount = e.progressNormalized;
    }

    private void Update()
    {
        Vector3 lookDirection = transform.position - Player.Instance.transform.position;
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _Outline.enabled = true;
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        _Outline.enabled = false;
    }

}
