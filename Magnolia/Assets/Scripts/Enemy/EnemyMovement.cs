using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform goal;

    private NavMeshAgent agent;
    private Rigidbody m_Rigidbody;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {       
        agent.destination = goal.position;
        transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        if (agent.isOnOffMeshLink)
        {
            Debug.Log("esta en el salto");
            m_Rigidbody.velocity = Vector3.up * 500 * Time.fixedDeltaTime;
        }
    }
}
