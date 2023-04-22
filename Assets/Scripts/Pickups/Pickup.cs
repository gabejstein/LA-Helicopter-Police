using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GS_Helicopter;
using System;

public abstract class Pickup : MonoBehaviour
{
    public Action OnPickedup;
    [SerializeField] protected string itemName = "Item Name";

    protected GameObject playerObject;
    protected DialogueBox dialogueBox;
    protected virtual void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        dialogueBox = FindObjectOfType<DialogueBox>();
    }
    public virtual void Process()
    {
        if (dialogueBox != null)
            dialogueBox.DisplayDialogue("You got " + itemName);
        else
            Debug.Log("Dialogue Box not found");

        OnPickedup?.Invoke();

        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            Process();
 
    }

    
}
