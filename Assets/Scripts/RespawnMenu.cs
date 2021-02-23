using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnMenu : MonoBehaviour
{
    public GameObject respawnMenuUI;
    public GameObject HUD;

    private bool showCursor = false;

    public void Respawn()
    {
        Time.timeScale = 1f;
        respawnMenuUI.SetActive(false);
        HUD.SetActive(true);
        showCursor = false;
    }

    public void StopGame()
    {
        Debug.Log("Stopping Game");
        Time.timeScale = 0f;
        respawnMenuUI.SetActive(true);
        HUD.SetActive(false);
        showCursor = true;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application Exited");
    }

    private void Update()
    {
        if (showCursor)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
