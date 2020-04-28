using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedualWalk : MonoBehaviour
{
    public Transform[] IKControls;
    public Transform[] StepPoints;
    public Transform[] DesiredPoints;

    [Header("Parameters")]
    //DesiredPoints
    public LayerMask LayerMask;

    //StepPoints
    public float StepLength;

    //IKControls
    public float StepSpeed;
    public float MaxLegRaise;
    public AnimationCurve AnimCurve;
    Vector3[] m_CurPoints;

    private void Awake()
    {
        int num = IKControls.Length;
        m_CurPoints = new Vector3[num];
    }
    private void Update()
    {
        UpdateDesiredPoints();
        UpdateStepPoints();
        UpdateIKControls();
    }

    private void UpdateIKControls()
    {
        for (int i = 0; i < IKControls.Length; i++)
        {
            float dist = Vector3.Distance(IKControls[i].position, StepPoints[i].position);            
            // Move the foot
            if (dist > 0.05f)
            {
                m_CurPoints[i] = Vector3.MoveTowards(m_CurPoints[i], StepPoints[i].position, StepSpeed * Time.deltaTime);
                float ratio = (StepLength - dist) / StepLength;
                IKControls[i].position = new Vector3(m_CurPoints[i].x, m_CurPoints[i].y + MaxLegRaise * AnimCurve.Evaluate(ratio), m_CurPoints[i].z);
            }
            // Clamp the foot        
            else
            {
                IKControls[i].position = StepPoints[i].position;
                m_CurPoints[i] = StepPoints[i].position;
            }
        }
    }

    private void UpdateStepPoints()
    {
        for (int i = 0; i < StepPoints.Length; i++)
        {
            float dist;
            dist = Vector3.Distance(DesiredPoints[i].position, StepPoints[i].position);
            if (dist > StepLength)
                StepPoints[i].position = DesiredPoints[i].position;
        }
    }

    private void UpdateDesiredPoints()
    {
        for (int i = 0; i < DesiredPoints.Length; i++)
        {
            float desiredYPosition;
            // Cast a ray
            RaycastHit hitInfo;
            Physics.Raycast(new Vector3(DesiredPoints[i].position.x, DesiredPoints[i].position.y + 50, DesiredPoints[i].position.z), -Vector3.up, out hitInfo, 100, LayerMask);

            // If we hit a collider, set the desiredYPosition to the hit Y point.        
            if (hitInfo.collider != null)
            {
                desiredYPosition = hitInfo.point.y;
            }
            else
            {
                desiredYPosition = DesiredPoints[i].position.y;
            }

            DesiredPoints[i].position = new Vector3(DesiredPoints[i].position.x, desiredYPosition, DesiredPoints[i].position.z);
        }
    }
}
