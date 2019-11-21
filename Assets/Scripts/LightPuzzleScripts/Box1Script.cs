using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box1Script : MonoBehaviour
{
    public bool activated = false;
    public GameObject[] affectedBoxes = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // If the box collider is overlapping with the player's collider
        if (GetComponent<Collider2D>().IsTouching(GameObject.FindWithTag("Player").GetComponent<Collider2D>()))
        {
            // If the user presses space
            if (Input.GetKeyDown("space"))
            {
                // Flip all the switches
                affectedBoxes[0].GetComponent<Box1Script>().activated = !affectedBoxes[0].GetComponent<Box1Script>().activated;
                affectedBoxes[1].GetComponent<Box2Script>().activated = !affectedBoxes[1].GetComponent<Box2Script>().activated;
                affectedBoxes[2].GetComponent<Box4Script>().activated = !affectedBoxes[2].GetComponent<Box4Script>().activated;
            }
        }

        if (activated)
        {
            GetComponent<SpriteRenderer>().color = new Color(0f, 250f, 0f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
        }
    }

}
