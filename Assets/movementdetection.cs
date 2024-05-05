using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementdetection : MonoBehaviour
{
    public GameObject illuminationmanager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBody"))
        {
            illuminationmanager.GetComponent<IlluminationManager>().setmovement(true);
        }
    }
}
