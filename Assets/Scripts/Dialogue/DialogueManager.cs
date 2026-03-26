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
    [SerializeField] Image _particleImage;
    [SerializeField] Animator _dialogueBoxAnimator;
    [SerializeField] Animator _characterAnimator;
    [SerializeField] Transform characterImage;
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
        _dialogueBoxAnimator.SetBool("isOpen", true);
        _characterAnimator.SetTrigger("newDialogue");

        _nameText.text = dialouge.speakerName;
        _characterImage.sprite = dialouge.speakerImage;
        _sentencesQueue.Clear();

        StartCoroutine((animateCharacter()));

        foreach (string sentence in dialouge.speakerText)
        {
            _sentencesQueue.Enqueue(sentence);
        }

        displayNextSentence();
    }

    void endDialouge()
    {
        _dialogueBoxAnimator.SetBool("isOpen", false);
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
            StopCoroutine(typeSentence(null));
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

    IEnumerator animateCharacter()
    {
        Vector3 startScale = new Vector3(0.8f, 0.8f, 0.8f);
        Vector3 endScale = new Vector3(1.2f, 1.2f, 1.2f);
        characterImage.transform.localScale = startScale;
        float animationDuration = 0.1f;
        float timer = 0;
        while (timer/animationDuration < 1)
        {
            characterImage.transform.localScale = Vector3.Lerp(startScale, endScale, timer);
            yield return null;
            timer += Time.deltaTime;
        }
        characterImage.transform.localScale = endScale;

        startScale = new Vector3(1.2f, 1.2f, 1.2f);
        endScale = Vector3.one;
        timer = 0;
        while (timer/animationDuration < 1)
        {
            characterImage.transform.localScale = Vector3.Lerp(startScale, endScale, timer);
            yield return null;
            timer += Time.deltaTime;
        }
        characterImage.transform.localScale = endScale;
    }

    public bool DialogueIsDone()
    {
        return _dialogueDone;
    }
}
