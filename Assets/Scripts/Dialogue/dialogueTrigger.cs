using System.Collections;
using UnityEngine;

public class dialogueTrigger : MonoBehaviour
{
    [SerializeField] DialogueSO _dialogueSO;
    BoxCollider _boxCollider;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    public IEnumerator StartDialogue(DialogueSO dialogueSO)
    {
        GameManager.Instance.SetGameIsDialogue();
        foreach (Dialogue d in dialogueSO.dialogueArray)
        {
            DialogueManager.Instance.StartDialouge(d);
            yield return new WaitUntil(() => DialogueManager.Instance.DialogueIsDone());

        }
        GameManager.Instance.SetGameIsPlaying();
        //Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerDialouge();
    }

    public void triggerDialouge()
    {
        StartCoroutine(StartDialogue(_dialogueSO));
    }
}
