using UnityEngine;
using UnityEngine.Playables;

public class CutSceneTrigger : MonoBehaviour
{
    [SerializeField] PlayableDirector _cutScene;
    public bool _playerCanMove;
    
    BoxCollider _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _cutScene.stopped += _cutScene_stopped;
    }

    private void _cutScene_stopped(PlayableDirector obj)
    {
        GameManager.Instance.SetGameIsPlaying();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_playerCanMove)
        {
            GameManager.Instance.SetGameIsCameraPan();
        }
        else
        {
            GameManager.Instance.SetGameIsCutscene();
        }
        _cutScene.Play();
        _collider.enabled = false;
    }
}
