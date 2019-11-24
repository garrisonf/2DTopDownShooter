﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Animator transitionAnim;

    RaycastHit2D hit;
    LineRenderer lineRend;
    bool laserAllowed = true;
    bool laserStarted = false;
    Vector2 laserStartingDirection;
    Ray2D ray;
    Color receiverColor;
    
    //Start is called before the first frame update
    void Start()
    {   
        lineRend = GetComponent<LineRenderer>();
        lineRend.enabled = false;
        laserStartingDirection = lineRend.GetPosition(lineRend.positionCount - 1) - lineRend.GetPosition(lineRend.positionCount - 2);
        ray = new Ray2D(lineRend.GetPosition(lineRend.positionCount - 2), laserStartingDirection);
        receiverColor = GameObject.FindWithTag("Receiver").GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown("r"))
       {
          Debug.Log("Laser Destroyed");
          destroyLaser();
       }
       
       if(laserAllowed && laserStarted)
       {
          RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
          
          if(hit.collider != null && hit.transform.tag == "Receiver")
          {
             laserStarted = false;
             lineRend.positionCount++;
             lineRend.SetPosition(lineRend.positionCount - 1, hit.point);
             hit.collider.gameObject.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
              
             Debug.Log("Laser Puzzle Completed");
             GameObject.Find("IslandCompletionManager").GetComponent<IslandCompletionTracker>().LaserPuzzleIslandCompleted = true;
             StartCoroutine(LoadScene());

                //StartCoroutine(waitForLaser());

            }

            if (hit.collider != null && hit.transform.tag == "Boundary")
          {
             Debug.Log(lineRend.positionCount);
             laserStarted = false;
             lineRend.positionCount++;
             Debug.Log(lineRend.positionCount); 
             lineRend.SetPosition(lineRend.positionCount - 1, hit.point);
             
             StartCoroutine(waitForLaser());
          }
          
          if(hit.collider != null && hit.transform.tag == "Reflector")
          {
             //to ignore raycast start in collider
             //Go to Edit -> Project Settings -> Physics2D -> Uncheck box "Queries Start in Colliders"
             //no need for hit.transform.gameObject.layer = 2;
             
             StartCoroutine(waitForLaser());
             
             Transform refTransform = hit.collider.transform;
             Debug.Log(hit.collider.name);
             
             Vector2 normalRef;
             //testCode alternative for hit.normal (normal of reflector)
             switch((int) refTransform.eulerAngles.z)
             {
               case 315:
               case -45:
                  normalRef = new Vector2(1, 1);
                  break;
               case -315:
               case 45:
                  normalRef = new Vector2(-1, 1);
                  break;
               case -225:
               case 135:
                  normalRef = new Vector2(1, 1);
                  break;
               case 225:
               case -135:
                  normalRef = new Vector2(-1, 1);
                  break;
               default:
                  Debug.LogError("Euler Angle: " +  (int) refTransform.eulerAngles.z);
                  normalRef = new Vector2(1, 1);
                  break;
             }
             
              Vector2 reflectedVector = Vector2.Reflect(ray.direction, normalRef.normalized);
             
             //Vector2 normalOfReflector = hit.normal;
             //Vector2 reflectedVector = Vector2.Reflect(ray.direction, normalOfReflector.normalized);
             
             (lineRend.positionCount)++;
             
             //lineRend.SetPosition(lineRend.positionCount - 1, hit.point);
             lineRend.SetPosition(lineRend.positionCount - 1, refTransform.position);
             
             
             
             //ray.origin = lineRend.GetPosition(lineRend.positionCount - 1);
             //ray.direction = reflectedVector;
             ray = new Ray2D(lineRend.GetPosition(lineRend.positionCount - 1), reflectedVector); 
          }
       }
    }
    
    public void OnCollisionStay2D(Collision2D other)
    {
       if(GetComponent<Collider2D>().IsTouching(GameObject.FindWithTag("Player").GetComponent<Collider2D>()))
       {
          if(Input.GetKeyDown("space"))
          {
             laserStarted = true;
             lineRend.enabled = true;
          }
       }
    }
    
    IEnumerator waitForLaser()
    {
       laserAllowed = false;
       yield return new WaitForSeconds(0.5f);
       laserAllowed = true;
    }
    
    public void destroyLaser()
    {
       lineRend.positionCount = 2;
       laserStarted = false;
       lineRend.enabled = false;
       laserStartingDirection = lineRend.GetPosition(lineRend.positionCount - 1) - lineRend.GetPosition(lineRend.positionCount - 2);
       ray = new Ray2D(lineRend.GetPosition(lineRend.positionCount - 2), laserStartingDirection);
       GameObject.FindWithTag("Receiver").GetComponent<SpriteRenderer>().color = receiverColor;
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("RyanHubWorld");
    }
}
