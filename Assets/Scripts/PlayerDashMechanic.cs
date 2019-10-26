using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashMechanic : MonoBehaviour
{
    private Camera m_MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        m_MainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseDirection = m_MainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (Input.GetKeyDown("left shift"))
        {
            GetComponent<Rigidbody2D>().AddForce(mouseDirection * 150);
        }
    }
}
