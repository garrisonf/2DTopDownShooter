using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCloudsParallax : MonoBehaviour
{
    private float length, startpos;

    public float scrollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float currentPosition = transform.position.x;


        transform.position = transform.position + new Vector3(scrollSpeed*Time.deltaTime, 0, 0);

        if (currentPosition < -length)
            transform.position = new Vector3(length, 0, 0);


    }
}
