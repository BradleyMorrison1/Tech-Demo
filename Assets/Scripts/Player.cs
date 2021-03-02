﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public GameObject spawnLocationObj;
    public GameObject respawnMenu;
    public CharacterController characterController;

    private float ammo;
    public float health;
    public float maxHealth;
    private float powerUpTimer = 10;
    private float colorTimer = 0; // timer used for color of health UI

    private bool startTimer = false;

    [SerializeField] private AudioClip gunShotSound;
    [SerializeField] private AudioClip reloadSound;

    public AudioSource audioSource;
    private Camera camera;

    public ParticleSystem muzzleFlash;
    public ParticleSystem bulletHit;
    public TrailRenderer bulletTracer;
    public Transform gunBarrel;


    public TMP_Text ammoText;
    public TMP_Text healthText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillBox"))
        {
            Vector3 spawnDistance = spawnLocationObj.transform.position - gameObject.transform.position;
            characterController.Move(spawnDistance);
            respawnMenu.GetComponent<RespawnMenu>().StopGame();
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
        camera = Camera.main;

        maxHealth = 100;
        health = maxHealth;
        ammo = 60;
    }

    private void Update()
    {
        ammoText.text = ("Ammo: " + ammo.ToString());
        healthText.text = ("Health: " + health.ToString());
        if (health <= 0)
        {
            health = 0;
            respawnMenu.GetComponent<RespawnMenu>().StopGame();
        }

        // shoot gun
        if (Input.GetButtonDown("Fire1") && ammo > 0)
        {
            Vector3 ray = camera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));

            RaycastHit hit;

            muzzleFlash.Play();
            var tracer = Instantiate(bulletTracer, gunBarrel.position, Quaternion.identity);
            tracer.AddPosition(ray);

            PlayGunshotAudio();

            ammo--;
            if (Physics.Raycast(ray, camera.transform.forward, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                Debug.DrawLine(gunBarrel.position, hit.point, Color.red, 1.0f);
            }
            tracer.transform.position = hit.point;
            var hitEffect = Instantiate(bulletHit, hit.point, tracer.transform.rotation);
            bulletHit.Play();
        }






        // Power Up
        if (startTimer) // while power up is active
        {
            powerUpTimer -= Time.deltaTime;
            health = maxHealth;
            var tmpGUI = healthText.GetComponent<TextMeshProUGUI>();
            tmpGUI.color = Color.Lerp(Color.yellow, Color.white, colorTimer);
            if (colorTimer < 1) colorTimer += Time.deltaTime / powerUpTimer;
        }
        if (powerUpTimer <= 0)
        {
            colorTimer = 0;
            powerUpTimer = 0;
            startTimer = false;
        }
    }

    private void PlayGunshotAudio()
    {
        audioSource.clip = gunShotSound;
        audioSource.Play();
    }

    private void PlayReloadAudio()
    {
        audioSource.clip = reloadSound;
        audioSource.Play();
    }
}
