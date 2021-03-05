using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float timeUntilRespawn = 1f;

    public AudioSource powerUpSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PlaySound());
        }
    }

    private IEnumerator PlaySound()
    {
        if (!powerUpSound.isPlaying)
        {
            powerUpSound.Play();
        }

        while (powerUpSound.isPlaying)
        {
            yield return null;
        }

        gameObject.SetActive(false);
        Invoke("enableCollectible", timeUntilRespawn);
    }

    private void enableCollectible()
    {
        gameObject.SetActive(true);
    }
}
