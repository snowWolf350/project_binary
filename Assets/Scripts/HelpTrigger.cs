using System;
using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public class HelpTrigger : MonoBehaviour
{
    BoxCollider _boxCollider;
    [SerializeField] HelpSO _helpSO;

    public static event EventHandler<onHelpTriggeredEventArgs> onHelpEntered;
    public static event EventHandler onHelpExit;
    public class onHelpTriggeredEventArgs : EventArgs
    {
        public string helpInfo;
    }

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        onHelpEntered?.Invoke(this, new onHelpTriggeredEventArgs
        {
            helpInfo = _helpSO._helpInfo
        });
    }
    private void OnTriggerExit(Collider other)
    {
        onHelpExit?.Invoke(this, EventArgs.Empty);
    }
}
