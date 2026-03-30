using UnityEngine;
using UnityEngine.Events;

public class onTriggerEnterEvent : MonoBehaviour
{
    public UnityEvent onPlayerEnter;
    public UnityEvent onPlayerExit;
    public UnityEvent onStart;

    private void Start()
    {
        onStart.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        onPlayerEnter.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        onPlayerExit.Invoke();
    }
}
