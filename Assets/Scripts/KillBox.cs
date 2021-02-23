using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    public GameObject spawnLocationObj;
    private bool killboxTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.position = spawnLocationObj.transform.position;
            Debug.Log("Triggered KillBox");
            killboxTriggered = true;
        }
    }

    private void Update()
    {
        if(killboxTriggered)
        {
            PlayerDied();
        }
    }

    private void PlayerDied()
    {
        Time.timeScale = Mathf.Lerp(0f, 1f, 1f);
    }
}
