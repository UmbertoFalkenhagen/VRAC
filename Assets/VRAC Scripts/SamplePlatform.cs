using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using VRAC_Scripts;

public class SamplePlatform : MonoBehaviour
{
    
    public enum Instrumenttype
    {
        Drums,
        Guitar,
        Piano,
        Bass,
        String
    }
    public Instrumenttype instrumenttype;
    private string instrument;
    public bool isActive = false;
    public bool isPointedOn;
    private GameObject[] symbols;
    public GameObject platformSymbol;

    public Renderer[] childrenderers;

    private int counter;
    public List<GameObject> spheres;
    public Material passiveMaterial;


    // Start is called before the first frame update
    private void Awake()
    {
        symbols = GameObject.FindGameObjectsWithTag("Symbol");
        foreach (var symbol in symbols)
        {
            if (symbol.transform.parent == transform)
            {
                platformSymbol = symbol;
                foreach (var renderer in symbol.GetComponentsInChildren<Renderer>())
                {
                    renderer.enabled = false;
                }
            }
        }

        /*childrenderers = GetComponentsInChildren<Renderer>();
        foreach (var renderer in childrenderers)
        {
            renderer.enabled = false;
        }*/

    }

    void Start()
    {
        
       
        instrument = instrumenttype.ToString();
        /*switch (instrument)
        {
            case "Drums":
                this.GetComponentInChildren<Renderer>().material.color = Color.blue;
                break;
            case "Guitar":
                this.GetComponentInChildren<Renderer>().material.color = Color.red;    
                break;
            case "Piano":
                this.GetComponentInChildren<Renderer>().material.color = Color.green;   
                break;
            case "String":
                this.GetComponentInChildren<Renderer>().material.color = Color.yellow;      
                break;
            case "Bass":
                this.GetComponentInChildren<Renderer>().material.color = Color.cyan;
                break;
        }*/
        spheres = new List<GameObject>();
        spheres.AddRange(GameObject.FindGameObjectsWithTag(instrument.ToString()));
        Debug.Log("The Number of Samples found for " + name + " is: " + spheres.Count);
        foreach (var sphere in spheres)
        {
            float angle = counter * Mathf.PI * 2f / spheres.Count;
            Vector3 renderercenter = this.GetComponentInChildren<Renderer>().bounds.center;
            Vector3 newPos = new Vector3(renderercenter.x + Mathf.Cos(angle) * 0.8f, renderercenter.y+ 1.2f, renderercenter.z + Mathf.Sin(angle) * 0.8f);
            sphere.GetComponent<Transform>().SetParent(transform);
            sphere.GetComponent<Transform>().position = newPos;
            sphere.GetComponent<SampleCube>().SetOriginalPosition(); //this line and the one below are important to save original position and the given tag because they will be changed throughout the process
            sphere.GetComponent<SampleCube>().SetStandardTag();
            sphere.GetComponent<SampleCube>().SetOriginalColor();
            //sphere.GetComponent<SampleCube>().SetOriginalScale();
            sphere.GetComponent<SampleCube>().SetDefaultMaterial();
            sphere.GetComponent<SampleCube>().SetDefaultParent();
            sphere.GetComponent<AudioSource>().loop = true;
            sphere.GetComponent<AudioSource>().volume = 0;
            sphere.GetComponent<AudioSource>().Play();
            counter++;
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        spheres.Clear();
        spheres.AddRange(GameObject.FindGameObjectsWithTag(instrument.ToString()));
        if (isActive)
        {
            foreach (var sphere in spheres)
            {
                sphere.GetComponent<Renderer>().enabled = true;
                sphere.GetComponent<Renderer>().material = sphere.GetComponent<SampleCube>().defaultmaterial;
                sphere.GetComponent<Renderer>().material.color = sphere.GetComponent<SampleCube>().originalcolor;
                
            }
            platformSymbol.GetComponent<InstrumentSymbol>().isActive = false;
        }
        else if (isPointedOn)
        {
            foreach (var sphere in spheres)
            {
                sphere.GetComponent<Renderer>().enabled = true;
                sphere.GetComponent<Renderer>().material = passiveMaterial;
                //sphere.GetComponent<Renderer>().material.color = sphere.GetComponent<SampleCube>().originalcolor;
            }
        }
        else
        {
            foreach (var sphere in spheres)
            {
                sphere.GetComponent<Renderer>().enabled = false;
                

            }

            //if (FindObjectOfType<IlluminationManager>().GetComponent<IlluminationManager>().checkmovement())
            {
                platformSymbol.GetComponent<InstrumentSymbol>().isActive = true;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
