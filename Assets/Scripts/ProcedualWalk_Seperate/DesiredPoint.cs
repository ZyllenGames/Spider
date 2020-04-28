using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesiredPoint : MonoBehaviour
{
    float desiredYPosition;
    public LayerMask LayerMask;

    void Update()
    {
        // Cast a ray
        RaycastHit hitInfo;
        Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 50, transform.position.z), -Vector3.up, out hitInfo, 100, LayerMask);

        // If we hit a collider, set the desiredYPosition to the hit Y point.        
        if (hitInfo.collider != null)
        {
            desiredYPosition = hitInfo.point.y;
        }
        else
        {
            desiredYPosition = transform.position.y;
        }

        transform.position = new Vector3(transform.position.x, desiredYPosition, transform.position.z);
    }
}
