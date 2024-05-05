using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.XR;

public class AudioManagerVRAC : MonoBehaviour
{
    public Sound[] sounds;

    public GameObject DrumsPrefab;
    public GameObject PianoPrefab;
    public GameObject GuitarPrefab;
    public GameObject BassPrefab;
    public GameObject StringPrefab;
    
    private string instrument;
    private int i = 0;
    private Vector3 pos;
    [HideInInspector]
    public static Hashtable positionedSpheres = new Hashtable();

    public static AudioSource RemoveFromOrbitSounds;
    public static AudioSource AddToOrbitSound;
    


    // More information can be found here: https://www.youtube.com/watch?v=6OT43pvUyfY
    void Awake()
    {
        pos.Set(0,0,0);
        Debug.Log("~~~ Number of sounds = " + sounds.Length);
        foreach (Sound s in sounds) {

            if (s.instrumenttype.ToString().Equals("Drums"))
            {
                GameObject sphere = Instantiate(DrumsPrefab, pos, Quaternion.identity);
                AddSampleClips(sphere, s);
                positionedSpheres.Add(sphere.GetInstanceID(), false);
                sphere.tag = instrument; //Hier wird dann der Audiosource der Tag hinzugefügt. Das funktioniert aber nur wenn man vorher einen Tag im Inspector erstellt hat der z.B. Piano heißt.
                //Tags kann man im Inspector ganz oben hinzufügen. Dort lassen sich Tags auswählen und neue hinzufügen
                Debug.Log("["+sphere.tag + "] was added to the audio clip " + sphere.name);
            } 
            
            else if (s.instrumenttype.ToString().Equals("Guitar"))
            {
                GameObject sphere = Instantiate(GuitarPrefab, pos, Quaternion.identity);
                AddSampleClips(sphere, s);
                positionedSpheres.Add(sphere.GetInstanceID(), false);
                sphere.tag = instrument; 
                Debug.Log("["+sphere.tag + "] was added to the audio clip " + sphere.name);
            } 
            
            else if (s.instrumenttype.ToString().Equals("Piano"))
            {
                GameObject sphere = Instantiate(PianoPrefab, pos, Quaternion.identity);
                AddSampleClips(sphere, s);
                positionedSpheres.Add(sphere.GetInstanceID(), false);
                sphere.tag = instrument;
                Debug.Log("["+sphere.tag + "] was added to the audio clip " + sphere.name);
            } 
            
            else if (s.instrumenttype.ToString().Equals("Bass"))
            {
                GameObject sphere = Instantiate(BassPrefab, pos, Quaternion.identity);
                AddSampleClips(sphere, s);
                positionedSpheres.Add(sphere.GetInstanceID(), false);
                sphere.tag = instrument;
                Debug.Log("["+sphere.tag + "] was added to the audio clip " + sphere.name);
            } 
            
            else if (s.instrumenttype.ToString().Equals("String"))
            {
                GameObject sphere = Instantiate(StringPrefab, pos, Quaternion.identity);
                AddSampleClips(sphere, s);
                positionedSpheres.Add(sphere.GetInstanceID(), false);
                sphere.tag = instrument;
                Debug.Log("["+sphere.tag + "] was added to the audio clip " + sphere.name);
            }
        }
    }

    void Start() {
        
    }

    // Update is called once per frame
    /*public void Play(string _name) {
        Sound s = Array.Find(sounds, sound => sound.name == _name);
        if (s == null) {
            Debug.LogWarning("Sound " + _name + " not found!");
            return;
        }
        //s.source.Play(); //A sound can now be used by using the line "FindObjectOfType<AudioManager>().Play("Name of the sound");"
    }*/

    private void Update()
    {
        if (isPlayerMoving())
        {
            GameObject.Find("Feet").GetComponent<AudioSource>().volume = 1f;
        }
        else
        {
            GameObject.Find("Feet").GetComponent<AudioSource>().volume = 0;
        }
    }

    private Boolean isPlayerMoving()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight))
        {
            return true;
        } else
        {
            return false;
        }
    }

    private void AddSampleClips(GameObject sphere, Sound s) //Adds sound s to gameobject sphere
    {
        AudioSource audioSource = sphere.AddComponent<AudioSource>();
        audioSource.clip = s.clip;
        audioSource.volume = 0;
        audioSource.loop = true;
        audioSource.pitch = 1f;
        audioSource.name = s.name;
        audioSource.playOnAwake = true;
        audioSource.spatialBlend = 1.0f;
        audioSource.rolloffMode = AudioRolloffMode.Custom;
        audioSource.maxDistance = 115;
        Debug.Log("Added " + sphere.GetComponent<AudioSource>().clip.ToString() + " to the Object");
        
        //Adding filters to each sample
        //AudioLowPassFilter lowPassFilter = sphere.AddComponent<AudioLowPassFilter>();
        //lowPassFilter.cutoffFrequency = 22000f;
        //AudioReverbFilter reverbFilter = sphere.AddComponent<AudioReverbFilter>();
        //reverbFilter.reverbPreset = AudioReverbPreset.Room;
        //reverbFilter.reverbPreset = AudioReverbPreset.User;
        instrument = s.instrumenttype.ToString();
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    /*public void PlayAddingSound()
    {
        Play("AddingSound");
    }
    
    public void PlayRemovalSound()
    {
        Play("RemovalSound");
    }*/
}
