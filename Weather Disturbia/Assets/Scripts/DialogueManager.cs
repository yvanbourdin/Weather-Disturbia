using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;

    public static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is more than one instance of DialogueManager in the scene");
            return;
        }
        instance = this;

        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue _dialogue)
    {
        animator.SetBool("isOpen", true);

        nameText.text = _dialogue.name;

        sentences.Clear();

        foreach (string sentence in _dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string _sentence)
    {
        dialogueText.text = "";
        foreach (char letter in _sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null; // = skip 1 frame
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }
}
