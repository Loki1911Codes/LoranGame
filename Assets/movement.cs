using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
     public float moveSpeed = 5f; // Adjust this to change the speed of movement
    private bool movingForward = true;

    void Update()
    {
        if (movingForward)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Reverse direction when reaching the boundary
        if (other.CompareTag("Boundary"))
        {
            movingForward = !movingForward;
        }
    }
}
