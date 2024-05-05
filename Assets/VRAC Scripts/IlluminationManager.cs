using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlluminationManager : MonoBehaviour
{

    public GameObject CenterPlatform;

    public List<GameObject> SurroundingPlatforms;
    public List<GameObject> Orbits;

    private Animator centerplatformanimator;

    private Animator surroundingplatformanimator;

    private Animator orbitanimator;

    public static Boolean hasmoved;

    public Boolean iscircleclosed;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        hasmoved = false;
        centerplatformanimator = CenterPlatform.GetComponentInChildren<Animator>();
        centerplatformanimator.SetBool("gameStarted", true);
        
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }


    public bool checkmovement()
    {
        return hasmoved;
    }
    public void setmovement(bool state)
    {
        hasmoved = state;
        Debug.Log("Player has moved");
        int platformcounter = 0;
        foreach (var platform in SurroundingPlatforms)
        {
            surroundingplatformanimator = platform.GetComponentInChildren<Animator>();
            StartCoroutine(enableanimation(surroundingplatformanimator, "starting", true, platformcounter));
            StartCoroutine(enablerenderers(platform, true, platformcounter));
            platformcounter = platformcounter + 1;
        }

        //StartCoroutine(pulsatingstart(platformcounter+1));


    }

    IEnumerator enableanimation(Animator animator, string parameter, bool state, float delaytime)
    {
        yield return new WaitForSeconds(delaytime);
        animator.SetBool(parameter, state);
        
        Debug.Log(animator.gameObject.name + " was set to " + state.ToString());
        
    }

    IEnumerator enablerenderers(GameObject rendererobject, bool state, float delaytime)
    {
        yield return new WaitForSeconds(delaytime);
        foreach (var renderer in rendererobject.GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = state;
        }
    }

    IEnumerator pulsatingstart(float delaytime)
    {
        yield return new WaitForSeconds(delaytime);
        foreach (var platform in SurroundingPlatforms)
        {
            foreach (var animator in platform.GetComponentsInChildren<Animator>())
            {
                enableanimation(animator, "pulsate", true, 0);
                Debug.Log("changed " + animator.gameObject.name +  " to true");
            }
        }
    }
}
