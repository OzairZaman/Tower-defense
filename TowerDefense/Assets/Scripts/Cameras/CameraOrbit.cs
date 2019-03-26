using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour {

    public Camera attachedCamera; // this is for drag and drop in unity
    [Header("Orbit")]
    public float xSpeed = 120f, ySpeed = 120f;
    public float yMinLimit = -20f, yMaxLimit = 80f;
    [Header("Collision")]
    public bool cameraCollision = true; // is Camera Collision enabled?
    public bool ignoreTriggers = true; // will the sphere cast ignore triggers?
    public float castRadius = .3f; //Radius of shpere cast
    public float castDistance = 1000f; // Distance the cast travels
    public LayerMask hitLayers; // Layers that casting will hit

    private float originalDistance; // Record starting distance of camera
    private float distance; // Current Distance of camera
    private float x, y; // X and Y Mouse Rotation

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attachedCamera.transform.position, castRadius);
    }


    void Start ()
    {
        // Set original distance
        originalDistance = Vector3.Distance(transform.position, attachedCamera.transform.position);
        // Set X and Y degrees to current camera rotation
        x = transform.eulerAngles.y;
        y = transform.eulerAngles.x;
    }
	
	
	void Update ()
    {
        // is right mouse button presed
        if (Input.GetMouseButton(1))
        {
            //disable cursor
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            // orbit
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * xSpeed * Time.deltaTime;

            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

            // Rotate the transform using Euler angles
            this.transform.rotation = Quaternion.Euler(y, x, 0);
        }
        else
        {
            //enable cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
	}

    private void FixedUpdate()
    {
        /// Set distance to original distance
        distance = originalDistance;

        // Change distance to what we hit
        // Is camera collision enabled?
        if (cameraCollision)
        {
            // Create a ray starting from orbit position and going in the direction of the camera
            Ray camRay = new Ray(transform.position, -transform.forward);
            RaycastHit hit; // Stores the hit information after cast
            // Shoot a sphere behind the camera
            if (Physics.SphereCast(camRay, // Ray in the directon of camera
                                   castRadius, // How thicc the sphere is
                                   out hit, // The hit information collected
                                   castDistance, // How far the cast goes
                                   hitLayers, // What layers we're allowed to hit
                                   ignoreTriggers ? // Ignore triggers?
                                    QueryTriggerInteraction.Ignore // Ignore it!
                                   : // Else
                                    QueryTriggerInteraction.Collide)) // Don't ignore
            {
                //set distance to distance of hit
                distance = hit.distance;
            }
        }
        
        // Apply distance to cameras
        attachedCamera.transform.position = transform.position - transform.forward * distance;
    }
}
