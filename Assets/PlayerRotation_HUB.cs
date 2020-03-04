using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation_HUB : MonoBehaviour
{
    private Camera mainCam;
    public bool instant = false;
    public float speed = 7;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ray cameraRay = mainCam.ScreenPointToRay(Input.mousePosition);
        Plane screenFloor = new Plane(Vector3.forward, Vector3.zero);
        float rayLength;
        screenFloor.Raycast(cameraRay, out rayLength);
        Vector3 pointToLook = cameraRay.GetPoint(rayLength);
        Vector2 mouseDirection = pointToLook-transform.position;
        float mouseAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        Quaternion desiredRotation = Quaternion.AngleAxis(mouseAngle, Vector3.forward);
        if (instant)
        {
            transform.rotation = desiredRotation;
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                desiredRotation,
                speed * Time.deltaTime * Mathf.Rad2Deg
            );
        }
    }
}
