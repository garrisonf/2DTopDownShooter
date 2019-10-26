using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCopterMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Handles copter movement (kind of drifty, like a helicopter)
        if (Input.GetButton("Up"))
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * moveSpeed);
        if (Input.GetButton("Down"))
            GetComponent<Rigidbody2D>().AddForce(Vector2.down * moveSpeed);
        if (Input.GetButton("Left"))
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * moveSpeed);
        if (Input.GetButton("Right"))
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * moveSpeed);
    }
}
