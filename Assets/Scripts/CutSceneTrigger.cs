using UnityEngine;
using UnityEngine.Playables;

public class CutSceneTrigger : MonoBehaviour
{
    [SerializeField] PlayableDirector _cutScene;
    
    BoxCollider _collider;

    private void OnTriggerEnter(Collider other)
    {
        _cutScene.Play();
    }
}
