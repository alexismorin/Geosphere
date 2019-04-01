using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ship : MonoBehaviour
{

    public Transform geosphere;
    public Transform geosphereRoot;
    public Transform hand;
    public Transform geosphereWorldRoot;
    public bool holdingGeoSphere;


    public float maxSpeed = 10f;
    public float rotationSpeed = 0.01f;

    public float speed;
    float currentYRotation = 0f;
    Quaternion hostRotation;
    Rigidbody rb;
    ConstantForce cf;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cf = GetComponent<ConstantForce>();
    }

    public void Grabbed()
    {
        GrabGeosphere();
    }


    public void Released()
    {
        ReleaseGeosphere();
    }

    void GrabGeosphere()
    {
        print("grab");
        holdingGeoSphere = true;
    }

    void ReleaseGeosphere()
    {
        print("release");
        holdingGeoSphere = false;
    }

    void Update()
    {
        OVRInput.Update();

        float triggerInput = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);

        speed = Mathf.Lerp(0, maxSpeed, triggerInput);

        Vector3 atRestRotation = new Vector3(0f, currentYRotation, 0f);

        hostRotation = transform.rotation;

        if (holdingGeoSphere)
        {

            geosphere.position = hand.position;
            geosphere.rotation = hand.rotation;


            //      hostRotation = geosphere.localRotation * transform.rotation;
            //     transform.rotation = Quaternion.Slerp(transform.rotation, hostRotation, 0.5f * Time.deltaTime);




            currentYRotation = transform.localEulerAngles.y;

            //      

        }
        else
        {



            geosphere.rotation = Quaternion.Slerp(geosphere.rotation, geosphereRoot.rotation, 6f * Time.deltaTime);
            geosphere.position = Vector3.Slerp(geosphere.position, geosphereRoot.position, 6f * Time.deltaTime);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(atRestRotation), 1.5f * Time.deltaTime);

        }

    }

    void FixedUpdate()
    {


        hostRotation = geosphere.localRotation * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, hostRotation, 0.5f * Time.deltaTime);
    }

    void LateUpdate()
    {
        //   rb.AddRelativeForce(transform.forward * speed);
        Vector3 ajustedSpeed = new Vector3(0f, 0f, speed);
        cf.relativeForce = ajustedSpeed;
    }

}