using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public SpeakerProfile speaker;
    public string text;
    private DialogueOption[] options;
}