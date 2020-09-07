using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public Transform eyes;

    [Range(0f, 360f)]
    public float visionAngle = 60f;
    public float visionDistance = 10f;
    public bool detected;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        detected = false;
        Vector3 playerVector = player.position - eyes.position;

        if (Vector3.Angle(playerVector.normalized, eyes.forward) < visionAngle)
        {
            if (playerVector.magnitude < visionDistance)
            {
                detected = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (visionAngle <= 0f) return;

        float halfVisionAngle = visionAngle / 2;

        Vector3 p1, p2, p3, p4;

        p1 = PointForAngle(halfVisionAngle+90, visionDistance);
        p2 = PointForAngle(-halfVisionAngle+90, visionDistance);
        p3 = PointForAngle2(halfVisionAngle + 90, visionDistance);
        p4 = PointForAngle2(-halfVisionAngle + 90, visionDistance);

        Gizmos.color = detected ? Color.green : Color.red;
        Gizmos.DrawLine(eyes.position, eyes.position + p1);
        Gizmos.DrawLine(eyes.position, eyes.position + p2);
        Gizmos.DrawLine(eyes.position, eyes.position + p3);
        Gizmos.DrawLine(eyes.position, eyes.position + p4);

        Gizmos.color = detected ? Color.green : Color.blue;
        Gizmos.DrawLine(eyes.position, eyes.position + eyes.forward * visionDistance);
    }

    Vector3 PointForAngle(float angle, float distance)
    {
        return eyes.TransformDirection(new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad))) * distance;
    }
    Vector3 PointForAngle2(float angle, float distance)
    {
        return eyes.TransformDirection(new Vector3(0, Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad))) * distance;
    }
}
