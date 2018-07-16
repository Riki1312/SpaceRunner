using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HoverControl : MonoBehaviour {

    Rigidbody m_body;

    public float m_hoverForce = 9.0f;
    public float m_hoverHeight = 2.0f;
    public GameObject[] m_hoverPoints;

    float m_currThrust = 0.0f;

    public float m_turnStrength = 10f;
    float m_currTurn = 0.0f;

    int m_layerMask;

    void Start()
    {
        m_body = GetComponent<Rigidbody>();

        m_layerMask = 1 << LayerMask.NameToLayer("Characters");
        m_layerMask = ~m_layerMask;
    }

    void Update()
    {

    }

    void OnDrawGizmos()
    {
        // Hover Force
        RaycastHit hit;
        for (int i = 0; i < m_hoverPoints.Length; i++)
        {
            var hoverPoint = m_hoverPoints[i];
            if (Physics.Raycast(hoverPoint.transform.position, -Vector3.up, out hit, m_hoverHeight, m_layerMask))
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(hoverPoint.transform.position, hit.point);
                Gizmos.DrawSphere(hit.point, 0.5f);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(hoverPoint.transform.position,
                hoverPoint.transform.position - Vector3.up * m_hoverHeight);
            }
        }
    }

    void FixedUpdate()
    {
        // Hover Force
        RaycastHit hit;
        for (int i = 0; i < m_hoverPoints.Length; i++)
        {
            var hoverPoint = m_hoverPoints[i];

            if (Physics.Raycast(hoverPoint.transform.position, -Vector3.up, out hit, m_hoverHeight, m_layerMask))
                m_body.AddForceAtPosition(Vector3.up * m_hoverForce * (1.0f - (hit.distance / m_hoverHeight)), hoverPoint.transform.position);
            else
            {
                if (transform.position.y > hoverPoint.transform.position.y)
                    m_body.AddForceAtPosition(hoverPoint.transform.up * m_hoverForce, hoverPoint.transform.position);
                else
                    m_body.AddForceAtPosition(hoverPoint.transform.up * -m_hoverForce, hoverPoint.transform.position);
            }
        }

        // Forward
        if (Mathf.Abs(m_currThrust) > 0)
            m_body.AddForce(transform.forward * m_currThrust);

        // Turn
        if (m_currTurn > 0)
        {
            m_body.AddRelativeTorque(Vector3.up * m_currTurn * m_turnStrength);
        }
        else if (m_currTurn < 0)
        {
            m_body.AddRelativeTorque(Vector3.up * m_currTurn * m_turnStrength);
        }
    }
}
