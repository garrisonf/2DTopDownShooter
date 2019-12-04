using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter1Script : MonoBehaviour
{
    public float counter;
    public GameObject counterObject;

    public GameObject[] switches = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter = 0;
        if (switches[0].GetComponent<Switch1Script>().activated == true)
            counter += switches[0].GetComponent<Switch1Script>().counter1;
        if (switches[1].GetComponent<Switch2Script>().activated == true)
            counter += switches[1].GetComponent<Switch2Script>().counter1;
        if (switches[2].GetComponent<Switch3Script>().activated == true)
            counter += switches[2].GetComponent<Switch3Script>().counter1;
        if (switches[3].GetComponent<Switch4Script>().activated == true)
            counter += switches[3].GetComponent<Switch4Script>().counter1;

        Vector3 positionObject = new Vector3 (-16 + counter / 8, 5, 0);


        StartCoroutine(moveToPos(counterObject.transform, positionObject, 1.0f));

    }

    bool isMoving = false;

    IEnumerator moveToPos(Transform fromPosition, Vector3 toPosition, float duration)
    {
        //Make sure there is only one instance of this function running
        if (isMoving)
        {
            yield break; ///exit if this is still running
        }
        isMoving = true;

        float counter = 0;

        //Get the current position of the object to be moved
        Vector3 startPos = fromPosition.position;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            fromPosition.position = Vector3.Lerp(startPos, toPosition, counter / duration);
            yield return null;
        }

        isMoving = false;
    }
}
