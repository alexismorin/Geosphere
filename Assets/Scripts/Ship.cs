using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    public Transform geosphere;
    public Transform geosphereRoot;
    public bool holdingGeoSphere;
    public float maxSpeed = 10f;

    float speed;
    float currentYRotation = 0f;
    Quaternion hostRotation;
    Rigidbody rb;

    void Start () {
        rb = GetComponent<Rigidbody> ();
    }

    void GrabGeosphere () {
        rb.isKinematic = true;
        holdingGeoSphere = true;
    }

    void ReleaseGeosphere () {
        rb.isKinematic = true;
        holdingGeoSphere = false;
    }

    void Update () {

        speed = Mathf.Lerp (0, maxSpeed, 0.5f);

        Vector3 atRestRotation = new Vector3 (0f, currentYRotation, 0f);
        hostRotation = transform.rotation;

        if (holdingGeoSphere) {

            hostRotation = transform.rotation * geosphere.localRotation;
            transform.rotation = Quaternion.Slerp (transform.rotation, hostRotation, 0.5f * Time.deltaTime);

            currentYRotation = transform.localEulerAngles.y;

            rb.AddForce (transform.forward * speed);

        } else {

            geosphere.rotation = Quaternion.Slerp (geosphere.rotation, geosphere.parent.rotation, 2f * Time.deltaTime);
            geosphere.position = Vector3.Slerp (geosphere.position, geosphere.parent.position, 2f * Time.deltaTime);

            transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (atRestRotation), 1.5f * Time.deltaTime);

        }

    }

}