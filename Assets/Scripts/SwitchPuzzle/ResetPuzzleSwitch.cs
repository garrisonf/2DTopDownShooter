using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPuzzleSwitch : MonoBehaviour
{

    public Switch1Script switch1;
    public Switch2Script switch2;
    public Switch3Script switch3;
    public Switch4Script switch4;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If the user presses space
        if (Input.GetKeyDown("r"))
        {
            Debug.Log("Reset Puzzle!");
            switch1.activated = false;
            switch2.activated = false;
            switch3.activated = false;
            switch4.activated = false;

        }
    }
}
