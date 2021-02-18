using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;
    public GameObject platformDestination;

    private void Start()
    {
        startPos = gameObject.transform.position;
        endPos = platformDestination.transform.position;

    }

    private void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(endPos, new Vector3(4,4,1)); 
    }
}
