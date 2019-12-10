using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCopterMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float maxSpeed = 30f;
    public float opposingForceMultiplier = 2;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Handles copter movement (kind of drifty, like a helicopter)
        Vector2 inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        var oppositeDirAmount = Mathf.Clamp01(Dot(rb.velocity.normalized, -inputDirection));
        var effectiveSpeed = Mathf.Lerp(moveSpeed, moveSpeed * opposingForceMultiplier, oppositeDirAmount);
        rb.AddForce(inputDirection * effectiveSpeed * Time.deltaTime, ForceMode2D.Impulse);
    }

    void FixedUpdate(){
        if(rb.velocity.magnitude > maxSpeed){
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private float Dot(Vector3 first, Vector3 second){
        return first.x * second.x + first.y * second.y + first.z * second.z;
    }
}
