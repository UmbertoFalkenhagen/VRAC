
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.Audio;
using OVRTouchSample;
using VRAC_Scripts;


public class SampleCube : MonoBehaviour
{
    public GameObject parenthand;
    public static bool placingonorbit = false;
    public static bool removingfromorbit = false;
    
    private bool isActive = false;
    private Vector3 originalpos;

    private Vector3 originalscale;
    private Vector3 currentscale;
    public Color originalcolor;
    
    public Material defaultmaterial;
    public Material grabbedMaterial;
    public Material activeMaterial;

    public GameObject scaledversionprefab;
    private float scalingfactor;
    
    private GameObject defaultparent;
    private AudioReverbFilter defaultreverb;
    private AudioLowPassFilter defaultlowpass;

    private AudioReverbFilter parentreverbfilter;
    private AudioLowPassFilter parentlowpassfilter;

    public List<GameObject> collidingobjects = null;
    
    

    private string _standardTag;

    private GameObject _nextposition;

    private GameObject _parentobject;
    // Start is called before the first frame update
    void Start()
    {
        SetStandardTag();
        SetOriginalPosition();
        //SetOriginalScale();
        //SetCurrentScale();
        SetOriginalColor();
        //SetDefaultFilters();
        



    }

    // Update is called once per frame
    void Update()
    {
        if (this.CompareTag("ActiveSampleCube"))
        {
            if (HandExtensionNew.grabbedSampleCubes.Length != 0 )
            {
                this.GetComponent<AudioSource>().volume = 0.4f;
            }
            else
            {
                this.GetComponent<AudioSource>().volume = 0.8f;
            }

            GetComponent<MeshRenderer>().material = activeMaterial;
        }
        else if (CompareTag("GrabbedSampleCube"))
        {
            GetComponent<MeshRenderer>().material = grabbedMaterial;
        }
        else
        {
            GetComponent<MeshRenderer>().material = defaultmaterial;
        }

        if (transform.parent != null)
        {
            if (this.transform.parent.name == "CustomHandLeft" || this.transform.parent.name == "CustomHandRight" )
            {
                parenthand = transform.parent.gameObject;
            }
        }
         
        
        
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            PlaySample(1f); //TODO change back to 1f
            if (!collidingobjects.Contains(col.gameObject)) //this make sure that the object is only added once
            {
                collidingobjects.Add(col.gameObject);
                tag = "GrabbedSampleCube";
                Debug.Log("The object " + col.gameObject.tag + " was added to the list of colliding objects of the object " + this.name);
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (this.transform.IsChildOf(col.gameObject.transform))
            {
                Debug.Log("The sphere " + this.name + " is currently linked to " + col.transform.parent.name + " and will not be moved back to its original position.");
                return;
            }
            else
            {
                if (collidingobjects.Contains(col.gameObject)) //this makes sure that the object is only removed if existing in the list (so to avoid nullexceptions i guess)
                {
                    collidingobjects.Remove((col.gameObject));
                    Debug.Log("The object " + col.gameObject.tag + " was removed from the list of colliding objects of the object " + this.name);
                }
                if (HandExtensionNew.closestPlacementSphere != null)
                {
                    Debug.Log("Attaching Sample to PlacementSphere");

                   
                    
                    if (parenthand.name == "CustomHandLeft")
                    {
                        VibrationManager.singleton.TriggerVibration(200, 2, 255, OVRInput.Controller.LTouch);
                    } else if (parenthand.name == "CustomHandRight")
                    {
                        VibrationManager.singleton.TriggerVibration(200, 2, 255, OVRInput.Controller.RTouch);
                    }
                    

                    this.GetComponent<Transform>().position = HandExtensionNew.closestPlacementSphere.transform.position; 
                    this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    this.GetComponent<Transform>().parent = HandExtensionNew.closestPlacementSphere.transform;


                    //adjusting the filter settings to its current parents filter settings (Orbit-specific) 
                    this.GetComponent<AudioSource>().rolloffMode = AudioRolloffMode.Custom;
                    this.GetComponent<AudioSource>().maxDistance = 115;
                    
                    parentreverbfilter = GetComponentInParent<AudioReverbFilter>();
                    AudioReverbFilter reverbFilter = this.gameObject.AddComponent<AudioReverbFilter>();
                    if (parentreverbfilter != null)
                    {
                        reverbFilter.decayTime = parentreverbfilter.decayTime;
                        reverbFilter.reverbDelay = parentreverbfilter.reverbDelay;
                    }
                    
                    parentlowpassfilter = GetComponentInParent<AudioLowPassFilter>();
                    AudioLowPassFilter lowPassFilter = this.gameObject.AddComponent<AudioLowPassFilter>();
                    if (parentlowpassfilter != null)
                    {
                        lowPassFilter.cutoffFrequency = parentlowpassfilter.cutoffFrequency;
                    }
                    
                    //Debug.Log("Reverbdecaytime was set to " + this.GetComponent<AudioReverbFilter>().decayTime);
                    //Debug.Log("Reverbdelay was set to " + this.GetComponent<AudioReverbFilter>().reverbDelay);
                    //Debug.Log("CutoffFrequency was set to " +  this.GetComponent<AudioLowPassFilter>().cutoffFrequency);
                    
                    //spawning the scaled version of the prefab scaled accordingly to the scalingfactor of the parent orbit
                    scalingfactor = GetComponentInParent<OrbitManager>().scalingfactor;
                    var clone = new GameObject();
                    clone = Instantiate(scaledversionprefab, transform.position, Quaternion.identity, transform);
                    clone.tag = "ScaledSample";
                    
                    var scale = new Vector3();
                    scale.x = clone.transform.localScale.x * scalingfactor;
                    scale.y = clone.transform.localScale.y * scalingfactor;
                    scale.z = clone.transform.localScale.z * scalingfactor;
                    clone.transform.localScale = scale;
                    
                    
                    
                    
                    

                        //scalingfactor = this.GetComponentInParent<OrbitManager>().scalingfactor;
                        //SetCurrentScale();
                        //SetOriginalScale();
                        //this.transform.localScale = currentscale;
                        
                        //FindObjectOfType<AudioManagerVRAC>().PlayAddingSound();
                        //TODO testen ob sample besser erkennbar und nach entfernen wieder normal groß wird
                        //this.GetComponent<Transform>().localScale = this.GetComponent<Transform>().localScale * 2f;
                        tag = "ActiveSampleCube";
                        HandExtensionNew.closestPlacementSphere = null;
                }
                else if (HandExtensionNew.closestPlacementSphere == null && !CompareTag("ActiveSampleCube"))
                {
                    ResetSample(" there was no placementsphere to attach to.", false);
                }
            }
            
        }
     

    }

    
    
    
    
    
    
    public void SetOriginalPosition()
    {
        originalpos = this.transform.position;
    }
    

    public void SetStandardTag()
    {
        _standardTag = this.tag;
    }

    public void SetOriginalColor()
    {
        originalcolor = this.GetComponent<Renderer>().material.color;
    }

    public void SetDefaultMaterial()
    {
        defaultmaterial = this.GetComponent<Renderer>().material;
    }

    public void SetDefaultParent()
    {
        defaultparent = this.GetComponent<Transform>().parent.gameObject;
    }

    public void ResetSample(string reason, Boolean destroychildren)
    {
        this.transform.position = originalpos;
        
        //this.transform.localScale = originalscale;

        Debug.Log("The defaultparent of " +  name + " is " + defaultparent.name);
        this.transform.parent = defaultparent.transform;
        
        
        if (this.GetComponent<AudioReverbFilter>() != null)
        {
            Destroy(GetComponent<AudioReverbFilter>());
        }

        if (this.GetComponent<AudioLowPassFilter>() != null)
        {
            Destroy(GetComponent<AudioLowPassFilter>());
        }

        if (destroychildren)
        {
            this.GetComponentInChildren<ScaledSample>().removechildren();
        }
        //this.transform.localScale = originalscale;
        //this.transform.localScale = originaltransform.lossyScale;
        this.GetComponent<Renderer>().material.color = originalcolor;
        Debug.Log("Sphere was moved to " + originalpos.ToString());
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        PlaySample(0f);
        this.collidingobjects.Clear();
        //FindObjectOfType<AudioManagerVRAC>().PlayRemovalSound();
        _parentobject = defaultparent;
        if (this.transform.position == originalpos)
        {
            this.tag = _standardTag;
            Debug.Log("The Objects tag was changed to " + this.tag);
        }
        Debug.Log("The sample was resetted because" + reason);
    }

    public String PlaySample(float volume)
    {
        this.GetComponent<AudioSource>().volume = volume;
        return "yes";
    }

    

}