using System.Collections;
using System.Collections.Generic;
using Mono.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //public Text nameText;
    public Text dialogueText;
    public GameObject dialogueUI;

    Queue<string> sentences;

    public GameObject g;
    CharacterScript characterController;

    void Start()
    {
        sentences = new Queue<string>();
        characterController = g.GetComponent<CharacterScript>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;  
        }
    }

    void EndDialogue()
    {
        characterController.onDialogue = false;
        dialogueUI.SetActive(false);
        Debug.Log("End of Convo");
    }

}
