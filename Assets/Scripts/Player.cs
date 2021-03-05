using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public GameObject spawnLocationObj;
    public GameObject respawnMenu;
    public CharacterController characterController;

    private float ammo;
    private float gunAmmo; // ammo in magazine of gun
    [SerializeField] private float maxGunAmmo = 15; // maximum amount of ammo in the magazine
    [SerializeField] private float maxAmmo = 330; // maximum amount of ammo in the possible
    private float gunAmmoDifference; // used when you reload while not having an empty mag
    private float ammoDifference; // used to calculate how ammo when reserve is less than 15
    public float health;
    public float maxHealth;
    private float powerUpTimer = 10; // time that the powerup stays active
    private float colorTimer = 0; // timer used for color of health UI
    [SerializeField]private float fireRate = 30f;
    private float nextTimeToFire = 0f;


    private bool startTimer = false;
    private bool isReloading = false;

    [SerializeField] private AudioClip gunShotSound;
    [SerializeField] private AudioClip reloadSound;
    [SerializeField] private AudioClip emptySound;

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
        ammo = 6;
        gunAmmo = maxGunAmmo;
    }

    private void Update()
    {
        ammoText.text = ("Ammo: " + gunAmmo.ToString() + " | " + ammo.ToString());
        healthText.text = ("Health: " + health.ToString());
        if (health <= 0)
        {
            health = 0;
            respawnMenu.GetComponent<RespawnMenu>().StopGame();
        }

        // shoot gun
        if (Input.GetButtonDown("Fire1") && gunAmmo > 0 && !isReloading && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        
        else if (Input.GetButtonDown("Fire1") && gunAmmo == 0 && !isReloading && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            PlayGunEmptyAudio();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            if (gunAmmo < maxGunAmmo && !isReloading && ammo > 0)
            {
                Reload();
            }
        }


        // range checking for ammo values
        if (ammo <= 0) ammo = 0;
        if (ammo >= maxAmmo) ammo = maxAmmo;
        if (gunAmmo <= 0) gunAmmo = 0;

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

    private void Shoot()
    {
        Vector3 ray = camera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));

        RaycastHit hit;

        muzzleFlash.Play();
        var tracer = Instantiate(bulletTracer, gunBarrel.position, Quaternion.identity);
        tracer.AddPosition(ray);

        PlayGunshotAudio();

        gunAmmo--;
        if (Physics.Raycast(ray, camera.transform.forward, out hit))
        {
            Debug.Log(hit.collider.gameObject.name);
            Debug.DrawLine(gunBarrel.position, hit.point, Color.red, 1.0f);
        }
        tracer.transform.position = hit.point;
        var hitEffect = Instantiate(bulletHit, hit.point, tracer.transform.rotation);
        bulletHit.Play();
    }


    private void Reload()
    {
        isReloading = true;
        PlayReloadAudio();
        if(audioSource.clip == reloadSound && audioSource.isPlaying)
        {
            isReloading = true;
            Debug.Log("Reloading");
        }
        Invoke("DoneReload", 0.7f);

    }

    private void DoneReload()
    {
        Debug.Log("Done Reloading");
        isReloading = false;
        gunAmmoDifference = maxGunAmmo - gunAmmo;
        if (ammo < 15f && ammo > gunAmmo)
        {
            gunAmmo += ammo;
            ammo -= ammo;
        }
        else if (ammo < 15f && gunAmmoDifference > ammo)
        {
            gunAmmo += ammo;
            ammo -= ammo;
        }
        else
        {
            gunAmmo += gunAmmoDifference;
            ammo -= gunAmmoDifference;
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

    private void PlayGunEmptyAudio()
    {
        if (audioSource.isPlaying && audioSource.clip == emptySound) return;
        audioSource.clip = emptySound;
        audioSource.Play();
    }
}
