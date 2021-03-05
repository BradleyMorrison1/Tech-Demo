using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject AmmoBox;
    public GameObject PowerUp;

    void Start()
    {
        //Instantiate(AmmoBox, new Vector3(-5f, 1f, 3f), Quaternion.identity);

        Instantiate(PowerUp, new Vector3(0f, 1f, 5f), Quaternion.identity);
    }
}
