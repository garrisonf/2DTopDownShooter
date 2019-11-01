using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratedDialogue : MonoBehaviour
{
    [System.Serializable]
    public class Dialogue
    {
        [TextArea(3, 10)]
        public string text;
        public SpeakerProfile speaker;
    }

    [Header("Dialogue Stuff")]
    public Dialogue[] dialogues;
    public float charDelay;
    private float timeToNextChar;
    private int dialogueIndex = -1;
    private int charIndex = -1;
    public Text textbox, nameText;

    [Header("Speech Stuff")]
    private float pitch, pitchVariance, speed;

    public AudioClip[] phonemes;
    private AudioSource aS;

    void Start()
    {
        aS = GetComponent<AudioSource>();
        textbox.text = ""; // clear text
    }

    void Update()
    {
        timeToNextChar -= Time.deltaTime;
        
        // add char if ready
        if (dialogueIndex != -1 && timeToNextChar <= 0f && charIndex < dialogues[dialogueIndex].text.Length - 1)
        {
            AddCharacter();    
        }

        // move to next dialogue
        if (Input.GetKeyDown(KeyCode.Space))
            NextDialogue();
    }

    void NextDialogue()
    {
        charIndex = -1;
        dialogueIndex++;
        SwitchSpeaker();
    }

    void AddCharacter()
    {
        string currentText = dialogues[dialogueIndex].text;
        charIndex++;
        textbox.text = currentText.Substring(0, charIndex + 1);
        char letter = currentText[charIndex];

        // letters that require pausing
        if (letter == ',' || letter == '-' || letter == '.' || letter == '!' || letter == '?' || letter == ':')
        {
            timeToNextChar = charDelay * 8;
        }
        // numbers get slight pause (just sounds better this way)
        else if(char.IsNumber(letter))
        {
            timeToNextChar = charDelay * 3;
        }
        else
        {
            timeToNextChar = charDelay;
        }

        // if the current character is speakable, speak it
        if(char.IsLetterOrDigit(letter))
        {
            SpeakChar(letter);
        }

        // speed mult
        timeToNextChar /= speed;
    }

    void SpeakChar(char letter)
    {
        // letter index
        int index = char.ToUpper(letter) - 65; // converts letter to alphabet index. a = 0, b = 1, etc
        if(char.IsNumber(letter))
        {
            index = (int)char.GetNumericValue(letter) + 26; // if character is a number. Numbers are stored in phonemes 27 - 36
        }

        aS.pitch = pitch;
        aS.pitch += Random.Range(-pitchVariance, pitchVariance);
        aS.PlayOneShot(phonemes[index]);
    }

    // setup voice properties from speaker profile
    void SwitchSpeaker()
    {
        SpeakerProfile profile = dialogues[dialogueIndex].speaker;
        pitch = profile.pitch;
        pitchVariance = profile.pitchVariance;
        speed = profile.speed;
        nameText.text = "- - " + profile.name + " - -";
    }
}