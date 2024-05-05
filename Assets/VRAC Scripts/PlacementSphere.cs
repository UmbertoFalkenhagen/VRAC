using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSphere : MonoBehaviour
{
    private Material currentmaterial;

    public Material activeMaterial; //this material will be applied while the player points on the sphere directly

    public Material passiveMaterial; //this material will be applied while the player holds a sample
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HandExtensionNew.grabbedSampleCubes.Length != 0)
        {
            this.GetComponent<MeshRenderer>().enabled = true;
            this.GetComponent<MeshRenderer>().material = passiveMaterial;
            
            if (this.gameObject == HandExtensionNew.closestPlacementSphere)
            {
                this.GetComponent<MeshRenderer>().material = activeMaterial;

            }
        }
        else
        {
            this.GetComponent<MeshRenderer>().enabled = false;
        }
        
    }
    
}
