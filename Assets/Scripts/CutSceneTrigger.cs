using UnityEngine;
using UnityEngine.Playables;

public class CutSceneTrigger : MonoBehaviour
{
    [SerializeField] PlayableDirector _cutScene;
    
    BoxCollider _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        _cutScene.Play();
        _collider.enabled = false;
    }
}
