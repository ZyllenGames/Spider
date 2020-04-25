using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float Speed;
    float curAngleY;

    Rigidbody m_Rigidbody;
    bool m_HasInput = false;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 dir = input.normalized;
        m_HasInput = input.magnitude != 0;

        float targetAngleY = 90 - Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        curAngleY = Mathf.Lerp(curAngleY, targetAngleY, 0.2f);
    }

    private void FixedUpdate()
    {
        if(m_HasInput)
        {
            m_Rigidbody.MoveRotation(Quaternion.Euler(Vector3.up * curAngleY));
            m_Rigidbody.MovePosition(m_Rigidbody.position + transform.forward * Speed * Time.fixedDeltaTime);
        }
    }
}
