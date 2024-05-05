using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public enum Instrumenttype
    {
        Drums,
        Guitar,
        Piano,
        Bass,
        String, 
        Ambient
    } //Hier wird bestimmt welche Instrumententypen zur Auswahl stehen

    public Instrumenttype instrumenttype; // Dadurch lässt sich im Inspector ein Instrumententyp auswählen
    public AudioClip clip;

    [Range(0f, 1f)] public float volume;

    [Range(0.1f, 3f)] public float pitch;

    public bool loop;


    [HideInInspector] public AudioSource source;
}