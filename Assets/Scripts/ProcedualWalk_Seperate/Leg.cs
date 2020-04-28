using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour
{
    public float Speed;
    public float MaxLegRaise;
    public Transform CurrentLock;
    public AnimationCurve AnimCurve;
    float timer;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentLock.position, Speed * Time.deltaTime);

        float dist = Vector3.Distance(transform.position, CurrentLock.position);

        // Move the foot
        if (dist > 0.1f)
        {
            // Increase curve timer
            timer += Time.deltaTime;
            // Move towards desired target position
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(CurrentLock.position.x, CurrentLock.position.y + AnimCurve.Evaluate(timer) * MaxLegRaise, CurrentLock.position.z), Speed * Time.deltaTime);

        }
        // Clamp the foot        
        else
        {
            transform.position = CurrentLock.position;
            timer = 0;
        }

    }
}
