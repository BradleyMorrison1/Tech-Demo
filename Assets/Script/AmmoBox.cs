using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    // Variables

    // used in the movement scripts
    private float rotationSpeed = 100f;
    private float moveSpeed = 0.5f;
    private bool startedMoveLoop;
    private bool moveDown;
    private bool moveUp;
    private Vector3 startPos;

    // used in respawning
    public float timeUntilRespawn = 1f;

    private void Start()
    {
        startPos.y = gameObject.transform.position.y; // Allows prefab to be placed at any height
    }

    private void Update()
    {
        // Used to rotate the box
        gameObject.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
        // For Moving the box up then down
        if (!startedMoveLoop) gameObject.transform.Translate(Vector3.up * Time.deltaTime);
        if (gameObject.transform.position.y >= startPos.y + 0.5)
        {
            startedMoveLoop = true;
            moveDown = true;
            moveUp = false;
        }
        else if (gameObject.transform.position.y <= startPos.y)
        {
            moveUp = true;
            moveDown = false;
        }
        if (moveDown) gameObject.transform.Translate(Vector3.down * Time.deltaTime * moveSpeed) ;
        if (moveUp) gameObject.transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Invoke("enableCollectible", timeUntilRespawn);
        }
    }

    private void enableCollectible()
    {
        gameObject.SetActive(true);
    }
}
