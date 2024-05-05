using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBorder : MonoBehaviour
{

    private Boolean isVisible = false;
    
    // Start is called before the first frame update
    void Start()
    {

        isVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isVisible)
        {
            this.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            this.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBody"))
        {
            isVisible = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBody"))
        {
            isVisible = false;
        }
    }
}
