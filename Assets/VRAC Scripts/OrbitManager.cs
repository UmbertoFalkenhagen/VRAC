using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitManager : MonoBehaviour
{
    [Range(2,20)]public int dockingpoints;
    [Range(0.1f,5f)] public float scalingfactor;
    [Range(0.1f, 1f)] public float rotationspeed;
    

    public GameObject PlacementSpherePrefab;
    private List<GameObject> children = null;
    private GameObject[] childrenarray;
    private bool isVisible = false;

    public Material activeMaterial;
    public Material passiveMaterial;

    private bool isRotating = false;
    private bool waspressed = false;

    [Range(5f, 100f)]public float radius;
    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> children = new List<GameObject>();
        /*var transformLocalScale = this.transform.localScale;
        transformLocalScale.x = radius;
        transformLocalScale.y = this.transform.localScale.y;
        transformLocalScale.z = radius;*/
        for (int i = 0; i < dockingpoints; i++)
        {
            //positioning placementspheres on orbit
            GameObject clone;
            var emptyObject = new GameObject();
            float angle = i * Mathf.PI * 2f / dockingpoints;
            Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, transform.position.y, Mathf.Sin(angle) * radius);
            emptyObject.transform.position = newPos;
            emptyObject.transform.parent = transform;
            clone = Instantiate(PlacementSpherePrefab, newPos, Quaternion.identity, emptyObject.transform);
            //scaling placementsphere prefab according to scalingfactor of the orbit
            var scale = new Vector3();
            scale.x = clone.transform.localScale.x * scalingfactor * 2;
            scale.y = clone.transform.localScale.y * scalingfactor * 2;
            scale.z = clone.transform.localScale.z * scalingfactor * 2;
            clone.transform.localScale = scale;
            
            
            children.Add(clone);
            Debug.Log(clone.name + " was added to Childlist of " + name);
        }
        childrenarray = children.ToArray();

        

    }

    // Update is called once per frame
    void Update()
    {
        
        
        foreach (var child in childrenarray)
        {
            if (child == HandExtensionNew.closestPlacementSphere)
            {
                isVisible = true;
            }
            else
            {
                isVisible = false;
            }
        }

        if (isVisible)
        {
            this.GetComponent<MeshRenderer>().material = activeMaterial;
        }
        else
        {
            this.GetComponent<MeshRenderer>().material = passiveMaterial;
        }
        
        OVRInput.Update();
        if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch) && !waspressed)
        {
            VibrationManager.singleton.TriggerVibration(200, 2, 255, OVRInput.Controller.RTouch);
            isRotating = !isRotating;
            waspressed = true;
            StartCoroutine(unblockorbitrotation(1f, false));
        }

        if (isRotating)
        {
            transform.Rotate(0f, 0f, rotationspeed, Space.Self);
        }


    }
    
    IEnumerator unblockorbitrotation(float delaytime, bool value)
    {
        yield return new WaitForSeconds(delaytime);
        this.waspressed = value;
        Debug.Log("Was pressed is now " + waspressed);
    }

    

    public void startrotation()
    {
        isRotating = !isRotating;
    }
}
