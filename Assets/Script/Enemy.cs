using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 10f;
    public float  turningSpeedVariation = 0.8f;

    Transform target;
    Transform nextTarget;
    int waypointIndex = 0;
    bool isTurning = false;

    // Start is called before the first frame update
    void Start()
    {
        target = Waypoint.waypoints[0];
        nextTarget = Waypoint.waypoints[1];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        // turn
        if (Vector3.Distance(transform.position, target.position) <= 3.5f)
        {
            Vector3 newDirection = nextTarget.position - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(newDirection);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * turnSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            isTurning = true;
            speed *= turningSpeedVariation;
        }
        else if (transform.rotation != target.rotation)
        {
            Quaternion newRotation = Quaternion.LookRotation(direction);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * turnSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            isTurning = true;
            speed *= turningSpeedVariation;
        }

        //movement
        transform.Translate(speed * Time.deltaTime * direction.normalized, Space.World);
        if(isTurning)
        {
            speed /= turningSpeedVariation;
            isTurning = false;
        }
        
        //select next waypoint
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
    }

    private void GetNextWayPoint()
    {
        if (waypointIndex >= Waypoint.waypoints.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        waypointIndex++;
        target = Waypoint.waypoints[waypointIndex];
        if (waypointIndex < Waypoint.waypoints.Length - 1)
            nextTarget = Waypoint.waypoints[waypointIndex + 1];
    }
}
