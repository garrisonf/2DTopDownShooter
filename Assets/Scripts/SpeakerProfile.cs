using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerProfile : MonoBehaviour
{
    public Animator DialStart;

    public SpriteRenderer SpeakerSprite;

    [Header("Speech Stuff")]

    [Range(1, 3)]
    public float pitch = 3.5f;
    [Range(0, 2)]
    public float pitchVariance = 0.5f;
    [Range(0.5f, 1.5f)]
    public float speed = 1f;
    public string name;

}