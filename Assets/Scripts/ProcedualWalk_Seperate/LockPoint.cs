using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPoint : MonoBehaviour
{
    public Transform DesiredPoint;
    public float MaxDistToMove;
    
    void Update()
    {
        float dist;

        dist = Vector3.Distance(DesiredPoint.position, transform.position);

        if (dist > MaxDistToMove)
        {
            transform.position = DesiredPoint.position;
        }
    }
}
