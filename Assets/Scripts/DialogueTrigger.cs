using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
            Debug.Log("Dialogue Area Entered");
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }
    //public void TriggerDialogue()
    //{
    //    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    //}
}
