using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Makrov : MonoBehaviour
{

    public float raycastDistance = Mathf.Infinity;
    public float shotForce = 10f;
    public Transform weaponPivot;
    public Camera Cam;
    public  AudioClip shooting;
    public AudioSource shot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }   

    public void FollowCamera()
    {

    }

    public void Fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
        {
            // Check if the ray hits a rigidbody
            Rigidbody hitRigidbody = hit.collider.GetComponent<Rigidbody>();
            if (hitRigidbody != null)
            {
                // Apply an impulse force to the rigidbody at the hit point
                hitRigidbody.AddForceAtPosition(transform.forward * shotForce, hit.point, ForceMode.Impulse);
            }
        }
        shot.Play();
    }
}
