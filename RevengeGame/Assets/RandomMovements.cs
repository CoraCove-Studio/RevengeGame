using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovements : MonoBehaviour 
{
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    public float speed = 5f;
    public float rotationSpeed = 100f;

    private void Update()
    {
        // Get the distance between the moving entity and the target waypoint
        float distanceToWaypoint = Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position);

        // If the entity is close enough to the waypoint, proceed to the next one
        if (distanceToWaypoint < 1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        // Calculate the desired rotation to face the target waypoint
        Quaternion desiredRotation = Quaternion.LookRotation(waypoints[currentWaypointIndex].position - transform.position);

        // Smoothly rotate the player towards the target waypoint
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);

        // Move the player towards the target waypoint
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

}
