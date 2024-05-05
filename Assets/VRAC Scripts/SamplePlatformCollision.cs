using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRAC_Scripts;

public class SamplePlatformCollision : MonoBehaviour
{
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
//        Debug.Log(other.name + " is colliding with " + name);
        if (other.CompareTag("PlayerBody"))
        {
            GetComponentInParent<SamplePlatform>().isActive = true;
            //Debug.Log("Player is colliding with " + name);
        } else if (other.CompareTag("HandExtension"))
        {
            if (!GameManager.isOnePlatformActive)
            {
                GetComponentInParent<SamplePlatform>().isPointedOn = true;
            }
            else
            {
                GetComponentInParent<SamplePlatform>().isPointedOn = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
//        Debug.Log(other.name + " stopped colliding with " + name);
        if (other.CompareTag("PlayerBody"))
        {
            //Debug.Log("Player stopped colliding with " + name);
            GetComponentInParent<SamplePlatform>().isActive = false;
        } else if (other.CompareTag("HandExtension"))
        {
            
            GetComponentInParent<SamplePlatform>().isPointedOn = false;
        }
    }
}
