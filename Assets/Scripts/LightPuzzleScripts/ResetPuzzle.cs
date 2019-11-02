using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPuzzle : MonoBehaviour
{

    public Box1Script box1;
    public Box2Script box2;
    public Box3Script box3;
    public Box4Script box4;
    public Box5Script box5;
    public Box6Script box6;
    public Box7Script box7;
    public Box8Script box8;
    public Box9Script box9;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Collider2D>().IsTouching(GameObject.FindWithTag("Player").GetComponent<Collider2D>()))
        {
            // If the user presses space
            if (Input.GetKeyDown("space"))
            {
                Debug.Log("Reset Puzzle!");
                box1.activated = false;
                box2.activated = false;
                box3.activated = false;
                box4.activated = false;
                box5.activated = false;
                box6.activated = false;
                box7.activated = false;
                box8.activated = false;
                box9.activated = false;

            }
        }
    }
}
