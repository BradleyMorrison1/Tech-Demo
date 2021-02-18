using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float timeUntilRespawn = 1f;

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
