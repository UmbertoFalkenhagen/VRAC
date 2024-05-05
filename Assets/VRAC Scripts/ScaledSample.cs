using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaledSample : MonoBehaviour
{

    public AudioSource parentsource;

    public AudioReverbFilter parentreverb;

    public AudioLowPassFilter parentlowpass;
    
    // Start is called before the first frame update
    void Start()
    {
        /*
        //copying audiosource and filters from original sample
        parentsource = GetComponentInParent<AudioSource>();
        parentreverb = GetComponentInParent<AudioReverbFilter>();
        parentlowpass = GetComponentInParent<AudioLowPassFilter>();
        
        //pasting them onto this object
        AudioSource newsource = this.gameObject.AddComponent<AudioSource>();
        AudioReverbFilter newreverb = this.gameObject.AddComponent<AudioReverbFilter>();
        AudioLowPassFilter newlowpass = this.gameObject.AddComponent<AudioLowPassFilter>();
        newsource = parentsource;
        newreverb = parentreverb;
        newlowpass = parentlowpass;
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScaledSample()
    {
        GetComponentInParent<SampleCube>().ResetSample("the scaled sample was destroyed", false);
        Destroy(this.gameObject);
    }

    public void removechildren()
    {
        Destroy(this.gameObject);
    }
    
    
}
