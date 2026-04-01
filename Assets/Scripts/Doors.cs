using UnityEngine;

public class Doors : MonoBehaviour,IHackable
{
    bool _isopen;
    [SerializeField]Animator _animator;
    [SerializeField] HackableUI _hackableUI;

    private void Start()
    {
        Player.OnHackableChanged += Player_OnHackableChanged;
        _hackableUI.Hide();
    }

    private void Player_OnHackableChanged(object sender, Player.OnHackableChangedEventArgs e)
    {
        if ((IHackable)this == e.hackable)
        {
            _hackableUI.Show();
        }
        else
        {
            _hackableUI.Hide();
        }
    }
    public void Interact()
    {
        _isopen = !_isopen;
        _animator.SetBool("doorIsOpen", _isopen);
    }
}
