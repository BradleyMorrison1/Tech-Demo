using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject movingDoor;
    public float moveSpeed = 1f;

    private Vector3 startPos;
    private bool doorDoneMoving;

    public bool openingDoor;
    public bool closingDoor;

    public AudioSource doorOpenSound;

    private void Start()
    {
        startPos = movingDoor.transform.position;
        //OpenDoor();
    }

    private void Update()
    {
        if (openingDoor) OpenDoor();
        if (closingDoor) CloseDoor();

    }

    private void OpenDoor()
    {
             
         if (movingDoor.transform.position.y < (startPos.y + 2f))
         {
            if(!doorOpenSound.isPlaying) doorOpenSound.Play();
            
            movingDoor.transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
         }
         else
         {
            openingDoor = false;
             Debug.Log("Stopped");
         }
    }

    private void CloseDoor()
    {
        if (movingDoor.transform.position.y > startPos.y)
        {
            movingDoor.transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
        }
        else
        {
            closingDoor = false;
            Debug.Log("Stopped");
        }
    }
}
