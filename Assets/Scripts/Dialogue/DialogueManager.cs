using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] TextMeshProUGUI _nameText;
    [SerializeField] TextMeshProUGUI _dialougeText;
    [SerializeField] Image _characterImage;
    [SerializeField] Image _speakerImage;
    [SerializeField] Animator _animator;
    bool _dialogueon;
    bool _dialogueDone = false;
    float _typingSpeed = 0.02f;


    private Queue<string> _sentencesQueue;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        _sentencesQueue = new Queue<string>();
    }

    public void StartDialouge(Dialogue dialouge)
    {
        _dialogueDone = false; // reset when a new dialogue starts
        _dialogueon = true;
        _animator.SetBool("isOpen", true);

        _nameText.text = dialouge.speakerName;
        _characterImage.sprite = dialouge.speakerImage;
        _sentencesQueue.Clear();

        foreach (string sentence in dialouge.speakerText)
        {
            _sentencesQueue.Enqueue(sentence);
        }

        displayNextSentence();
    }

    void endDialouge()
    {
        _animator.SetBool("isOpen", false);
        _dialogueon = false;
        _dialogueDone = true; // signal that this dialogue is done
    }


    public void displayNextSentence()
    {
        if (!_dialogueon) return;

            if (_sentencesQueue.Count == 0)
            {
                endDialouge();
                return;
            }
            string sentence = _sentencesQueue.Dequeue();
            StopAllCoroutines();
            StartCoroutine(typeSentence(sentence)); 
    }

    IEnumerator typeSentence(string sentence)
    {
        _dialougeText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            _dialougeText.text += letter;
            yield return new WaitForSeconds(_typingSpeed);
        }
    }

    public bool DialogueIsDone()
    {
        return _dialogueDone;
    }
}
