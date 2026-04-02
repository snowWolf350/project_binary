using UnityEngine;
using UnityEngine.UI;

public class HackableUI : MonoBehaviour
{
    [SerializeField] Image _fillImage;

    private void Start()
    {
        GameInput.Instance.onProgressChanged += Instance_onProgressChanged;
    }

    private void Instance_onProgressChanged(object sender, IHasProgress.onProgressChangedEventArgs e)
    {
        _fillImage.fillAmount = e.progressNormalized;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
