using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandExtensionNew : MonoBehaviour
{
    
    public List<GameObject> collidingPlacementSpheresList = null;
    public GameObject[] collidingPlacementSpheresArray = null;
    public static GameObject closestPlacementSphere = null;
    
    public static GameObject[] grabbedSampleCubes = null;
    public GameObject[] activeSampleCubes;

    public List<GameObject> collidingActiveSampleCubesList = null;
    public GameObject[] collidingActiveSampleCubesArray = null;
    public static GameObject closestActiveSampleCube = null;

    
    // Start is called before the first frame update
    void Start()
    {
        grabbedSampleCubes = GameObject.FindGameObjectsWithTag("GrabbedSampleCube");
        collidingPlacementSpheresList = new List<GameObject>();
        collidingActiveSampleCubesList = new List<GameObject>();
        //Invoke("test", 2);
    }

    // Update is called once per frame
    void Update()
    {
        grabbedSampleCubes = GameObject.FindGameObjectsWithTag("GrabbedSampleCube");
        activeSampleCubes = GameObject.FindGameObjectsWithTag("ActiveSampleCube");

        if (grabbedSampleCubes.Length == 0)
        {
            if (collidingPlacementSpheresList.Count == 0)
            {
                
            }
        }
        
        if (grabbedSampleCubes.Length != 0) //all activesamplecubes play at half volume while another sample is hold by the player
        {
            foreach (var activeSampleCube in activeSampleCubes)
            {
                activeSampleCube.GetComponent<AudioSource>().volume = 0.5f;
            }
        }
        else
        {
            foreach (var activeSampleCube in activeSampleCubes)
            {
                activeSampleCube.GetComponent<AudioSource>().volume = 1f;
            }
        }
        OVRInput.Update();
        if (closestActiveSampleCube != null)
        {
            //Debug.Log("Pointing on at least one active sample cube");
            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
            {
                if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
                {
                    VibrationManager.singleton.TriggerVibration(200, 2, 255, OVRInput.Controller.LTouch);
                }
                else if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
                {
                    VibrationManager.singleton.TriggerVibration(200, 2, 255, OVRInput.Controller.RTouch);
                }
                
                
                
                Debug.Log("Zeigefinger was pressed");
                if (closestActiveSampleCube.CompareTag("ActiveSampleCube"))
                {
                    closestActiveSampleCube.GetComponent<SampleCube>().ResetSample(" a button was pressed.", true);
                } else if (closestActiveSampleCube.CompareTag("ScaledSample"))
                {
                    closestActiveSampleCube.GetComponent<ScaledSample>().ResetScaledSample();
                }
                
                Debug.Log("The Sample " + closestActiveSampleCube.name + " was resetted");
                if (collidingActiveSampleCubesList.Contains(closestActiveSampleCube.gameObject))
                {
                    collidingActiveSampleCubesList.Remove(closestActiveSampleCube.gameObject);
                    if (collidingActiveSampleCubesList.Count == 0)
                    {
                        closestActiveSampleCube = null;
                    }
                    else
                    {
                        collidingActiveSampleCubesArray = collidingActiveSampleCubesList.ToArray();
                        closestActiveSampleCube = collidingActiveSampleCubesArray[collidingActiveSampleCubesArray.Length-1];
                    }
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlacementSphere"))
        {
            
                if (!collidingPlacementSpheresList.Contains(other.gameObject))
                {
                    collidingPlacementSpheresList.Add(other.gameObject);
                    Debug.Log("Placementsphere was added to list of colliding objects");
                    closestPlacementSphere = other.gameObject;
                }
            
        }
        else if (other.CompareTag("ActiveSampleCube") || other.CompareTag("ScaledSample"))
        {
            if (!collidingActiveSampleCubesList.Contains(other.gameObject))
            {
                collidingActiveSampleCubesList.Add(other.gameObject);
                closestActiveSampleCube = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlacementSphere"))
        {
            if (collidingPlacementSpheresList != null)
            {
                if (collidingPlacementSpheresList.Contains(other.gameObject))
                {
                    collidingPlacementSpheresList.Remove(other.gameObject);
                    Debug.Log("Placementsphere was removed to list of colliding objects");
                    Invoke("Recalc", 2);

                }   
            }
            
        } else if (other.CompareTag("ActiveSampleCube") || other.CompareTag("ScaledSample"))
        {
            if (collidingActiveSampleCubesList.Contains(other.gameObject))
            {
                collidingActiveSampleCubesList.Remove(other.gameObject);
                if (collidingActiveSampleCubesList.Count == 0)
                {
                    closestActiveSampleCube = null;
                }
                else
                {
                    collidingActiveSampleCubesArray = collidingActiveSampleCubesList.ToArray();
                    closestActiveSampleCube = collidingActiveSampleCubesArray[collidingActiveSampleCubesArray.Length-1];
                }
            }
        }
    }

    public void Recalc()
    {
        if (collidingPlacementSpheresList.Count == 0)
        {
            closestPlacementSphere = null;
            Debug.Log("The closest Placementsphere was set to null");
        }
        else
        {
            collidingPlacementSpheresArray = collidingPlacementSpheresList.ToArray();
            closestPlacementSphere = collidingPlacementSpheresArray[0];
            Debug.Log("The closest Placementsphere was set to " + closestPlacementSphere);
        }
    }
    
}
