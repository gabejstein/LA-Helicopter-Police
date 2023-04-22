using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{

    [SerializeField] Text dialogueTextUI;
    [SerializeField] GameObject dialoguePanel;

    Queue<string> dialogueQueue = new Queue<string>(); //I thought I might need this in case there's a log-jam of dialogues being played at once.

    bool isPlaying = false;

    float dialogueSpeed = 0.6f;

    AudioSource audioSource;
    [SerializeField] AudioClip radioOpenSFX;

    // Start is called before the first frame update
    void Awake()
    {
        dialoguePanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    public void DisplayDialogue(string _text)
    {
        dialogueQueue.Enqueue(_text);

        if(!isPlaying)StartCoroutine(RunText());
    }

    IEnumerator RunText()
    {
        
        float waitTime = 0f;
        isPlaying = true;

        while(dialogueQueue.Count > 0)
        {
            dialoguePanel.SetActive(true);

            string text = dialogueQueue.Dequeue();
            int wordCount = text.Split(' ').Length;
            Debug.Log(text);

            //Calculate the waiting time based on the number of words or voice acting if there is any.
            waitTime = wordCount * dialogueSpeed;

            dialogueTextUI.text = text;

            PlaySound();

            yield return new WaitForSeconds(waitTime);

            dialoguePanel.SetActive(false);

            //Wait a little more before playing next dialogue.
            yield return new WaitForSeconds(0.5f);
        }

       isPlaying = false;

        yield return null;
    }

    void PlaySound()
    {
        if (audioSource == null) return;
        audioSource.PlayOneShot(radioOpenSFX);
    }
}
