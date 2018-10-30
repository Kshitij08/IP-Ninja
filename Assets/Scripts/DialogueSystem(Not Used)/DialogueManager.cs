using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;
    public bool destroyOnEnd = false;
    Scene scene;

    public GameObject littleGirlDialogueBox;


    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();

        scene = SceneManager.GetActiveScene();

    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
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

        string sentence =  sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        
    }

    IEnumerator TypeSentence (string sentence)
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
        if(scene.name == "Level1")
        {
            littleGirlDialogueBox.SetActive(false);
            SceneManager.LoadScene("Level2");
            destroyOnEnd = true;
        }

        if (scene.name == "Level2")
        {
            littleGirlDialogueBox.SetActive(false);
            SceneManager.LoadScene("Level1");
            destroyOnEnd = true;
        }

        Debug.Log("End of the Conversation.");


        if (destroyOnEnd)
        {
            Destroy(gameObject);
        }
        
    }


}
