using UnityEngine;

public class Doors : MonoBehaviour,IHackable
{
    bool _isopen;
    [SerializeField]Animator _animator;


    public void Interact()
    {
        _isopen = !_isopen;
        _animator.SetBool("doorIsOpen", _isopen);
    }
}
