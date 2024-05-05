
using UnityEngine;

public class PhysicsPointer : MonoBehaviour
{
    public float defaultLength = 3.0f;

    private LineRenderer _lineRenderer = null;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();

    }

    private void Update()
    {
        UpdateLength();
    }

    private void UpdateLength()
    {
     _lineRenderer.SetPosition(0, transform.position);
     _lineRenderer.SetPosition(1, CalculateEnd());
    }

    private Vector3 CalculateEnd()
    {
        RaycastHit raycastHit = CreateForwardRaycast();
        Vector3 endPosition = DefaultEnd(defaultLength);

        if (raycastHit.collider)
        {
            endPosition = raycastHit.point;
        }

        return endPosition;
    }

    private RaycastHit CreateForwardRaycast()
    {
        RaycastHit raycastHit;

        Ray ray = new Ray(transform.position, transform.forward);

        Physics.Raycast(ray, out raycastHit, defaultLength);
        return raycastHit;
    }

    private Vector3 DefaultEnd(float length)
    {
        return transform.position + (transform.forward * length);
    }
}