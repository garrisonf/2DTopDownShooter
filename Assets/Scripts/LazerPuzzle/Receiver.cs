using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Receiver : MonoBehaviour
{
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //if collision with LineRenderer, change color of sprite
        if(sr != null)
        {
           //sr.color = new Color(Random.value, Random.value, Random.value);
        }
    }
    
    public void randomColors()
    {
       sr.color = new Color(Random.value, Random.value, Random.value);
    }
}
