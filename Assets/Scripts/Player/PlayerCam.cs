using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
   public float sensitivity = 5.0f;
    public float maxYAngle = 90.0f;
    public float minYAngle = -90.0f;

    private float rotationX = 0.0f;
    public Transform cameraTransform;

    void Start()
    {
        cameraTransform = transform.Find("Main Camera"); // Assuming the camera is a child named "Camera"
        if (cameraTransform == null)
        {
            Debug.LogError("Camera object not found!");
            enabled = false;
        }
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotationX -= mouseY * sensitivity;
        rotationX = Mathf.Clamp(rotationX, minYAngle, maxYAngle);

        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.Rotate(Vector3.up * mouseX * sensitivity);

        
    }
}
