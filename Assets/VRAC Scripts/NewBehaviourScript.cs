using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Just for saving the old sample placement algorithm 
    /*    if (this.GetComponent<Transform>().parent != null)
            {
                _parentobject = this.GetComponent<Transform>().parent.gameObject; 
            }
            lefthandindexpressure = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
            righthandindexpressure = OVRInput.Get((OVRInput.Axis1D.SecondaryIndexTrigger));
            if (_parentobject != null)
            {
                if (_parentobject.CompareTag("Player"))
                {
                   MoveBetweenPositions(); 
                }
                if (lefthandindexpressure == 0f && _parentobject.name == "CustomHandLeft" || righthandindexpressure == 0f && _parentobject.name == "CustomHandRight")
                {
                    if (collidingobjects.Contains(_nextposition) && (_nextposition.name != "PlacementSphere 0-0"))
                    {
                        this.GetComponent<Transform>().parent = _nextposition.transform;
                        Debug.Log("The object " + this.name + " is now a child of " + this.transform.parent.name);
                    }
                    else
                    {
                        ResetSample();
                    }
                }
            }
    
            if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick))
            {
                i = 0;
                j = 0;
                ResetSample();
            } */
    
    /*public void MoveBetweenPositions() //used when moving the samples via button clicks
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Debug.Log("The A-Button on the controller was pressed");
            if (i < GameManager.GetLayers()-1)
            {
                i++;
            } else
            {
                i = 0;
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            if (j < 3 && i != 0) //has to be adjusted when the number of PlacementSpheres per Layer was changed
            {
                j++;
            } else if (j < 3 && i == 0)
            {
                j++;
                i = 1;
            }
            else
            {
                j = 0;
            }
        }
        Debug.Log("i="+i+ ", j="+j);
        _nextposition = GameObject.Find("PlacementSphere " + i + "-" + j);
        this.transform.position = _nextposition.transform.position;
        Debug.Log("The object was moved to the Position of " + _nextposition.name);
    } */
}
