using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class InstrumentSymbol : MonoBehaviour
{
    private float movespeed = 1f;
    private float rotationspeed = 0.2f;
    private Vector3 upperpos;
    private Vector3 lowerpos;
    private Vector3 startingposition;
    public bool isActive;
    private Renderer[] childrenderers;

    private Boolean movedirection = false;
    // Start is called before the first frame update
    void Start()
    {
        upperpos = transform.position;
        //upperpos.x += 0.5f;
        upperpos.y += 0.5f;
        //upperpos.z += 0.5f;

        lowerpos = transform.position;
        lowerpos.y -= 0.5f;

        startingposition = transform.position;
        startingposition.y = UnityEngine.Random.Range(-0.5f, 0.5f);
        transform.position = startingposition;

    }

    // Update is called once per frame
    void Update()
    {
        childrenderers = GetComponentsInChildren<Renderer>();
        float step = movespeed * Time.deltaTime;
        if (movedirection)
        {
            transform.position = Vector3.MoveTowards(transform.position, upperpos, step);
            if (Vector3.Distance(transform.position, upperpos) <= 0.1f)
            {
                movedirection = !movedirection;
            } else if ((Vector3.Distance(transform.position, upperpos) <= 0.2f) && (Vector3.Distance(transform.position, upperpos) > 0.1f))
            {
                movespeed = 0.3f;
            } else if ((Vector3.Distance(transform.position, upperpos) <= 0.3f) && (Vector3.Distance(transform.position, upperpos) > 0.2f))
            {
                movespeed = 0.6f;
            } else if ((Vector3.Distance(transform.position, upperpos) <= 0.9f) && (Vector3.Distance(transform.position, upperpos) > 0.7f))
            {
                movespeed = 0.6f;
            } else if ((Vector3.Distance(transform.position, upperpos) <= 0.7f) && (Vector3.Distance(transform.position, upperpos) > 0.3f))
            {
                movespeed = 1f;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, lowerpos, step);
            if (Vector3.Distance(transform.position, lowerpos) <= 0.1f)
            {
                movedirection = !movedirection;
            } else if ((Vector3.Distance(transform.position, lowerpos) <= 0.2f) && (Vector3.Distance(transform.position, lowerpos) > 0.1f))
            {
                movespeed = 0.3f;
            } else if ((Vector3.Distance(transform.position, lowerpos) <= 0.3f) && (Vector3.Distance(transform.position, lowerpos) > 0.2f))
            {
                movespeed = 0.6f;
            } else if ((Vector3.Distance(transform.position, lowerpos) <= 0.9f) && (Vector3.Distance(transform.position, lowerpos) > 0.7f))
            {
                movespeed = 0.6f;
            } else if ((Vector3.Distance(transform.position, lowerpos) <= 0.7f) && (Vector3.Distance(transform.position, lowerpos) > 0.3f))
            {
                movespeed = 1f;
            }
        }
        transform.Rotate(0f, rotationspeed, 0f, Space.Self);

        foreach (var renderer in childrenderers)
        {
            if (isActive)
            {
                renderer.enabled = true;
            }
            else
            {
                renderer.enabled = false;
            }
        }
        
    }
}
