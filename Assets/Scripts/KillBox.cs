using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    public GameObject spawnLocationObj;
    public GameObject respawnMenu;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other + "Triggered KillBox");
            other.gameObject.transform.position = spawnLocationObj.transform.position;
        }
    }

    private void PauseGame()
    {
            respawnMenu.GetComponent<RespawnMenu>().StopGame();
    }
}
