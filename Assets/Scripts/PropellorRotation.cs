using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellorRotation : MonoBehaviour
{
    public float rotateSpeed = 500f;
    public float x, y, z;
    private Vector3 turnDirection;
    // Start is called before the first frame update
    void Start()
    {
        turnDirection = new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(turnDirection * rotateSpeed * Time.deltaTime);
    }
}
