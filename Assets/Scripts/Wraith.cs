using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wraith : MonoBehaviour {

    GameObject[] waypoints;
    GameObject targetWaypoint;
    float distanceToWayPoint;
    public float wanderDistance = 20f; // max distance for a selected waypoint
    public float speed = 10f;
    public float rotationSpeed = 3f;
    Rigidbody rb;
    Animator an;

    void Start () {
        rb = GetComponent<Rigidbody> ();
        an = GetComponent<Animator> ();
        waypoints = GameObject.FindGameObjectsWithTag ("waypoint");
        FindNewWayPoint ();
    }

    void FindNewWayPoint () {
        targetWaypoint = null;
        while (targetWaypoint == null) {
            int dice = Random.Range (0, waypoints.Length);
            if (Vector3.Distance (transform.position, waypoints[dice].transform.position) < wanderDistance) {
                targetWaypoint = waypoints[dice];
            }
        }
        an.SetTrigger ("newWaypoint");
    }

    void Update () {

        if (targetWaypoint != null) {

            var targetRotation = Quaternion.LookRotation (targetWaypoint.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            rb.AddRelativeForce (Vector3.forward * speed);
            // transform.Translate (Vector3.forward * Time.deltaTime * speed, Space.Self);

            if (Vector3.Distance (transform.position, targetWaypoint.transform.position) < 3f) {
                FindNewWayPoint ();
            }

        }
    }
}