using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public GameObject spawnLocationObj;
    public CharacterController characterController;

    private float ammo;
    public float health;
    public float maxHealth;
    private float powerUpTimer = 30;

    private bool startTimer = false;

    public TMP_Text ammoText;
    public TMP_Text healthText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillBox"))
        {

            Vector3 spawnDistance = spawnLocationObj.transform.position - gameObject.transform.position;
            characterController.Move(spawnDistance);
            Debug.Log("Triggered KillBox");
        }

        else if (other.CompareTag("Checkpoint"))
        {
            spawnLocationObj.transform.position = other.transform.position;
            Debug.Log("Triggered Checkpoint");
        }

        else if (other.CompareTag("AmmoBox"))
        {
            ammo += 30;
            Debug.Log("Collected Ammo");
        }

        else if (other.CompareTag("PowerUp"))
        {
            startTimer = true;
        }
    }


    private void Start()
    {
        maxHealth = 100;
        health = maxHealth;
        ammo = 60;
    }

    private void Update()
    {
        ammoText.text = ("Ammo: " + ammo.ToString());
        healthText.text = ("Health: " + health.ToString());

        if (startTimer)
        {
            powerUpTimer -= Time.deltaTime;


        }
        if (powerUpTimer <= 0)
        {
            powerUpTimer = 0;
            startTimer = false;
        }
    }
}
