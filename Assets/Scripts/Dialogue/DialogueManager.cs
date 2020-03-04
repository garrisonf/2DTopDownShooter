//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;


//[System.Serializable]
//public class DialogueManager : MonoBehaviour
//{
//    public Animator animator;

//    [Header("Dialogue")]
//    public Text nameText;
//    public Text dialogueText;
//    public Dialogue[] dialogues;
//    public float charDelay;
//    private float timeToNextChar;
//    private int dialogueIndex = -1;
//    private int charIndex = -1;
//    //public Text textbox, nameText;

//    public Queue<string> sentences;

//    [Header("Speech")]
//    private float pitch, pitchVariance, speed;

//    public AudioClip[] phonemes;
//    private AudioSource aS;

//    // Start is called before the first frame update
//    void Start()
//    {
//        aS = GetComponent<AudioSource>();
//        sentences = new Queue<string>();
//    }

//    //void Update()
//    //{
//    //    timeToNextChar -= Time.deltaTime;

//    //    // add char if ready
//    //    if (dialogueIndex != -1 && timeToNextChar <= 0f && charIndex < dialogues[dialogueIndex].text.Length - 1)
//    //    {
//    //        AddCharacter();
//    //    }

//    //    // move to next dialogue
//    //    if (Input.GetKeyDown(KeyCode.Space))
//    //        NextDialogue();
//    //}

//    public void StartDialogue(Dialogue dialogue)
//    {
//        animator.SetBool("isOpen", true);

//        Debug.Log("Starting conversation with " + dialogue.name);

//        nameText.text = dialogue.name;

//        sentences.Clear();

//        foreach (string sentence in dialogue.sentences)
//        {
//            sentences.Enqueue(sentence);
//        }

//        DisplayNextSentence();
//    }

//    public void DisplayNextSentence()
//    {
//        if (sentences.Count == 0)
//        {
//            EndDialogue();
//            return;
//        }
//        string sentence = sentences.Dequeue();
//        Debug.Log(sentence);
//        StopAllCoroutines();
//        StartCoroutine(TypeSentence(sentence));
//    }

//    IEnumerator TypeSentence(string sentence)
//    {
//        dialogueText.text = "";
//        foreach (char letter in sentence.ToCharArray())
//        {
//            dialogueText.text += letter;
//            if (char.IsLetterOrDigit(letter))
//            {
//                SpeakChar(letter);
//            }
//            yield return null;
//        }

//    }

//    void SpeakChar(char letter)
//    {
//        // letter index
//        int index = char.ToUpper(letter) - 65; // converts letter to alphabet index. a = 0, b = 1, etc
//        if (char.IsNumber(letter))
//        {
//            index = (int)char.GetNumericValue(letter) + 26; // if character is a number. Numbers are stored in phonemes 27 - 36
//        }

//        aS.pitch = pitch;
//        aS.pitch += Random.Range(-pitchVariance, pitchVariance);
//        aS.PlayOneShot(phonemes[index]);
//    }

//    // setup voice properties from speaker profile
//    //void SwitchSpeaker()
//    //{
//    //    SpeakerProfile profile = dialogues[dialogueIndex].speaker;
//    //    pitch = profile.pitch;
//    //    pitchVariance = profile.pitchVariance;
//    //    speed = profile.speed;
//    //    nameText.text = "- - " + profile.name + " - -";
//    //}

//    void EndDialogue()
//    {
//        animator.SetBool("isOpen", false);
//        Debug.Log("End of conversation.");
//    }
//}
