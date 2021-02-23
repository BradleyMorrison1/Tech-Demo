using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Vector3 currentPos; // current position of platform
    private Vector3 startPos; // position at the start of the platform's path
    private Vector3 endPos; // position at the end of the platform's path
    private Vector3 handoffPos; // used to temporarily store the a position to allow it to swap
    private Vector3 calcPos; // used to calculate where the platform needs to go
    public float distance;

    public GameObject platformDestination; // empty game object used to determine where the platform will stop


    public float moveSpeed = 3f;
    //public float distance;

    public bool platformMoving = false;
    public bool destinationReached = false;
    public bool switchDirections = false;

    private void Start()
    {
        startPos = gameObject.transform.position;
        endPos = platformDestination.transform.position;
        calcPos = endPos - startPos;

    }

    private void Update()
    {
        currentPos = gameObject.transform.position;

        distance = Vector3.Distance(currentPos, endPos);

        if (platformMoving) MovePlatform();

        if (distance <= 0.05f) // platform reached its destination (can't use zero because of floating point inaccuracy)
        {
            platformMoving = false;

            Debug.Log("Platform Reached Destination");
            switchDirections = true;
            
        }

        if(switchDirections) SwitchDirections(); 
    }

    private void SwitchDirections()
    {
        handoffPos = startPos;
        startPos = endPos;
        endPos = handoffPos;

        calcPos = endPos - startPos;

        switchDirections = false;
    }

    private void MovePlatform()
    {
        gameObject.transform.Translate(calcPos * moveSpeed * Time.deltaTime);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Rigidbody>().MovePosition(player.transform.position += calcPos * moveSpeed * Time.deltaTime);
        //player.transform.parent = gameObject.transform;
    }
}
