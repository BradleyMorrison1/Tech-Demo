using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float timeUntilRespawn = 2f;
    public AudioSource hitSound;

    public void Hit()
    {
        StartCoroutine(PlaySound());
    }

    private IEnumerator PlaySound()
    {
        if (!hitSound.isPlaying)
        {
            hitSound.Play();
        }

        while (hitSound.isPlaying)
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
