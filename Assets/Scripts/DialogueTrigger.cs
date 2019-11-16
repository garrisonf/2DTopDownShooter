using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public void TriggerDialogue()
    {
        Debug.Log("starting");
        FindObjectOfType<GeneratedDialogue>().StartDialogue();
    }
}
