using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Audio;
using UnityEditor;

namespace VRAC_Scripts
{
    public class GameManager : MonoBehaviour
    {
        [HideInInspector] public static List<GameObject> spheres;
        [HideInInspector] public GameObject[] spherearray;

        public float ambientmovementintensity;

        private float circularAngle;
        private int counter;
        private float radius = 2;
        public float moveSpeed = 1;
        public static List<GameObject> placementspheres;
        public List<GameObject> sampleplatforms;
        public static bool isOnePlatformActive;
        private static int i= 0;

        private bool ambientpermanent = false;
        private float ambientvolume;

        private bool waspressed = false;
        
        

        

        private void Awake()
        {
        }

        // Start is called before the first frame update
        void Start()
        {
            ambientvolume = this.GetComponent<AudioSource>().volume;
            spheres = new List<GameObject>();
            spheres.AddRange(GameObject.FindGameObjectsWithTag("Drums"));
            spheres.AddRange(GameObject.FindGameObjectsWithTag("Guitar"));
            spheres.AddRange(GameObject.FindGameObjectsWithTag("Piano"));
            spheres.AddRange(GameObject.FindGameObjectsWithTag("String"));
            spheres.AddRange(GameObject.FindGameObjectsWithTag("Bass"));
            spherearray = spheres.ToArray();
            //spherearray[1].gameObject.tag = "GrabbedSampleCube";
            /*foreach (var sphere in spherearray)
            {
                
                if (sphere.CompareTag("Drums"))
                {
                    sphere.GetComponent<Renderer>().material.color = Color.blue;
                }
                else if (sphere.CompareTag("Guitar"))
                {
                    sphere.GetComponent<Renderer>().material.color = Color.red;
                }
                else if (sphere.CompareTag("Piano"))
                {
                    sphere.GetComponent<Renderer>().material.color = Color.green;
                } 
                else if (sphere.CompareTag("String"))
                {
                    sphere.GetComponent<Renderer>().material.color = Color.yellow;
                }
                else if (sphere.CompareTag("Bass"))
                {
                    sphere.GetComponent<Renderer>().material.color = Color.cyan;
                }
            } */
        }
       

        // Update is called once per frame
        void Update()
        {
            
            isOnePlatformActive = false;
            sampleplatforms.AddRange(GameObject.FindGameObjectsWithTag("SamplePlatform"));
            foreach (var platform in sampleplatforms)
            {
                if (platform.GetComponent<SamplePlatform>().isActive)
                {
                    isOnePlatformActive = true;
                }
            }
            //active sample volumes are lowered while another sample is hold by the player
           if (GameObject.FindGameObjectsWithTag("GrabbedSampleCube").Length != 0)
            {
                foreach (var samplecube in GameObject.FindGameObjectsWithTag("ActiveSampleCube"))
                {
                    samplecube.GetComponent<AudioSource>().volume = 0.5f;
                }

                foreach (var scaledsample in GameObject.FindGameObjectsWithTag("ScaledSample"))
                {
                    scaledsample.GetComponent<AudioSource>().volume = 0.5f;
                }
            }
            else
            {
                foreach (var samplecube in GameObject.FindGameObjectsWithTag("ActiveSampleCube"))
                {
                    samplecube.GetComponent<AudioSource>().volume = 1f;
                }
                foreach (var scaledsample in GameObject.FindGameObjectsWithTag("ScaledSample"))
                {
                    scaledsample.GetComponent<AudioSource>().volume = 1f;
                }
            }
           
           OVRInput.Update();
           if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.LTouch) && !waspressed)
           {
               VibrationManager.singleton.TriggerVibration(200, 2, 255, OVRInput.Controller.LTouch);
               ambientpermanent = !ambientpermanent;
               Debug.Log("Ambientpermanent was set to " + ambientpermanent.ToString());
               waspressed = true;
               StartCoroutine(unblockpressedbutton(1f, false));
           }

           if ((GameObject.FindGameObjectsWithTag("GrabbedSampleCube").Length != 0 || GameObject.FindGameObjectsWithTag("ActiveSampleCube").Length != 0) && !ambientpermanent)
           {
               
               if (ambientvolume > 0f) 
               {
                   this.GetComponent<AudioSource>().volume = ambientvolume;
                   ambientvolume = ambientvolume - 0.03f * Time.deltaTime; 
               }
               
           } else
           {
               if (ambientvolume < 0.1f)
               {
                   
                   ambientvolume = ambientvolume + 0.01f * Time.deltaTime;
               }
               this.GetComponent<AudioSource>().volume = ambientvolume;
           }

           
        }

        public static int GetLayers()
        {
            return placementspheres.Count / i;
        }

        IEnumerator unblockpressedbutton(float delaytime, bool value)
        {
            yield return new WaitForSeconds(delaytime);
            this.waspressed = value;
            Debug.Log("Was pressed is now " + waspressed);
        }
        
    }
}

