using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving2 : MonoBehaviour
{   public float moveSpeed = 5f; // Adjust this to change the speed of movement
    private bool movingForward = true;

    void Update()
    {
        if (movingForward)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(-Vector3.up * moveSpeed * Time.deltaTime);
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

