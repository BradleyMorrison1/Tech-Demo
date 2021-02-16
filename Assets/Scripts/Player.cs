using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public GameObject spawnLocationObj;
    public CharacterController characterController;

    private float ammo;
    public TMP_Text ammoText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillBox"))
        {

            Vector3 spawnDistance = spawnLocationObj.transform.position - gameObject.transform.position;
            characterController.Move(spawnDistance);
            Debug.Log("Triggered KillBox");
        }

        if (other.CompareTag("Checkpoint"))
        {
            spawnLocationObj.transform.position = other.transform.position;
            Debug.Log("Triggered Checkpoint");
        }

        if (other.CompareTag("AmmoBox"))
        {
            ammo += 30;
            Debug.Log("Collected Ammo");
        }
    }


    private void Start()
    {
        ammo = 60;
    }

    private void Update()
    {
        ammoText.text = ammo.ToString();
    }
}
