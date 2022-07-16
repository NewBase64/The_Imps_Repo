using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCamera : MonoBehaviour
{
    [Header("Sensitivity")]
    [Range(100, 1200)][SerializeField] int sensHori;
    [Range(100, 1200)][SerializeField] int sensVert;
    [Header("-----------")]
    [Header("Attributes")]
    [Range(10, 180)][SerializeField] public float fov;
    [Header("-----------")]
    [Header("Invert axis")]
    [SerializeField] bool invertY;
    [SerializeField] bool invertX;

    int lockVertMin = -90;
    int lockVertMax = 90;
    float xRot;
    float yRot;

    void Start()
    {
        // Lock the cursor and hide it during gameplay
        gamemanager.instance.LockCursor();
        Camera.main.fieldOfView = fov;
    }

    void LateUpdate()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensVert * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensVert * Time.deltaTime;

        // Invert Y
        if (invertY)
            xRot += mouseY;
        else
            xRot -= mouseY;

        // Clamp the camera so the player cannot look upsidedown
        xRot = Mathf.Clamp(xRot, lockVertMin, lockVertMax);

        // Rotate the camera on the X axis for vertical momvement
        transform.localRotation = Quaternion.Euler(xRot, 0, 0);

        // Invert X
        if (invertX)
            // Rotate the transform of the parent so that the player can walk towards where they look
            transform.parent.Rotate(Vector3.up * -mouseX);
        else
            transform.parent.Rotate(Vector3.up * mouseX);

        Camera.main.fieldOfView = fov;
    }
}
