using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EndingSceneAnimationManager : MonoBehaviour
{

    public GameObject[] availableDrumSamples;
    public GameObject[] availableBassSamples;
    public GameObject[] availableGuitarSamples;
    public GameObject[] availablePianoSamples;
    public GameObject[] availableStringSamples;

    public GameObject usedDrumSample;
    public GameObject usedBassSample;
    public GameObject usedGuitarSample;
    public GameObject usedPianoSample;
    public GameObject usedStringSample;
    
    public GameObject[] orbits;

    public GameObject[] placementspheres;

    public float timer = 60f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LateStart());

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        int seconds;
        seconds = (int)timer % 60;
        if (seconds < 12 && seconds > 0)
        {
            Debug.Log(seconds + " remaining till shutdown");

        }
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(1f);
        availableDrumSamples = GameObject.FindGameObjectsWithTag("Drums");
        availableBassSamples = GameObject.FindGameObjectsWithTag("Bass");
        availableGuitarSamples = GameObject.FindGameObjectsWithTag("Guitar");
        availablePianoSamples = GameObject.FindGameObjectsWithTag("Piano");
        availableStringSamples = GameObject.FindGameObjectsWithTag("String");

        usedDrumSample = availableDrumSamples[Random.Range(0, availableDrumSamples.Length - 1)];
        usedBassSample = availableBassSamples[Random.Range(0, availableBassSamples.Length - 1)];
        usedGuitarSample = availableGuitarSamples[Random.Range(0, availableGuitarSamples.Length - 1)];
        usedStringSample = availableStringSamples[Random.Range(0, availableStringSamples.Length - 1)];
        usedPianoSample = availablePianoSamples[Random.Range(0, availablePianoSamples.Length - 1)];

        orbits = GameObject.FindGameObjectsWithTag("Orbit");
        placementspheres = GameObject.FindGameObjectsWithTag("PlacementSphere");
        usedDrumSample.transform.parent = placementspheres[Random.Range(0, placementspheres.Length - 1)].transform;
        usedDrumSample.transform.position = usedDrumSample.transform.parent.position;
        
        usedBassSample.transform.parent = placementspheres[Random.Range(0, placementspheres.Length - 1)].transform;
        usedBassSample.transform.position = usedBassSample.transform.parent.position;
        usedPianoSample.transform.parent = placementspheres[Random.Range(0, placementspheres.Length - 1)].transform;
        usedPianoSample.transform.position = usedPianoSample.transform.parent.position;
        usedStringSample.transform.parent = placementspheres[Random.Range(0, placementspheres.Length - 1)].transform;
        usedStringSample.transform.position = usedStringSample.transform.parent.position;
        usedGuitarSample.transform.parent = placementspheres[Random.Range(0, placementspheres.Length - 1)].transform;
        usedGuitarSample.transform.position = usedGuitarSample.transform.parent.position;

        /*foreach (var orbit in orbits)
        {
            StartCoroutine(startrotationcoroutine(0f, orbit));
            StartCoroutine(startrotationcoroutine(59f, orbit));
        }*/

        usedDrumSample.tag = "ActiveSampleCube";
        usedBassSample.tag = "ActiveSampleCube";
        usedGuitarSample.tag = "ActiveSampleCube";
        usedStringSample.tag = "ActiveSampleCube";
        usedPianoSample.tag = "ActiveSampleCube";

        
        StartCoroutine(RemoveSample(59f, usedStringSample, "String"));
        StartCoroutine(RemoveSample(61f, usedBassSample, "Bass"));
        StartCoroutine(RemoveSample(63f, usedPianoSample, "Piano"));
        StartCoroutine(RemoveSample(65f, usedGuitarSample, "Guitar"));
        
        StartCoroutine(RemoveSample(67f, usedDrumSample, "Drums"));

    }

    IEnumerator RemoveSample(float delaytime, GameObject sample, String originaltag)
    {
        yield return new WaitForSeconds(delaytime);
        sample.tag = originaltag;
        sample.GetComponent<AudioSource>().volume = 0f;
    }
    
    IEnumerator startrotationcoroutine(float delaytime, GameObject orbit)
    {
        yield return new WaitForSeconds(delaytime);
        orbit.GetComponent<OrbitManager>().startrotation();
    }
}
