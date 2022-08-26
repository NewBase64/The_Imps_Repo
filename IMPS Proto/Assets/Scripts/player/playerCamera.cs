using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCamera : MonoBehaviour
{
    [Header("Sensitivity")]
    [Range(1, 1200)] public int sensHori;
    [Range(1, 1200)] public int sensVert;
    [Header("-----------")]
    [Header("Attributes")]
    [Range(10, 180)][SerializeField] public float fov;
    [Header("-----------")]
    [Header("Invert axis")]
    [SerializeField] bool invertY;
    [SerializeField] bool invertX;

    int lockVertMin = -90;
    int lockVertMax = 90;
    float yRot;

    public const string HorizontalSensitibility = "SensHori";
    public const string VerticalSensitibility = "SensVert";

    void Start()
    {
        // Lock the cursor and hide it during gameplay
        gamemanager.instance.LockCursor();
        Camera.main.fieldOfView = fov;
        LoadSensitivity();
    }

    void LateUpdate()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensVert * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensVert * Time.deltaTime;

        // Invert Y
        if (invertY)
            yRot += mouseY;
        else
            yRot -= mouseY;

        // Clamp the camera so the player cannot look upsidedown
        yRot = Mathf.Clamp(yRot, lockVertMin, lockVertMax);

        // Rotate the camera on the X axis for vertical momvement
        transform.localRotation = Quaternion.Euler(yRot, 0, 0);

        // Invert X
        if (invertX)
            // Rotate the transform of the parent so that the player can walk towards where they look
            transform.parent.Rotate(Vector3.up * -mouseX * sensHori * Time.deltaTime);
        else
            transform.parent.Rotate(Vector3.up * mouseX * sensHori * Time.deltaTime);

        Camera.main.fieldOfView = fov;

    }
    public void crouch()
    {
        transform.Translate(0, -1, 0);

    }
    public void goUP()
    {
        transform.Translate(0, 1, 0);
    }

    void LoadSensitivity()
    {
        int horiSens = PlayerPrefs.GetInt(HorizontalSensitibility, 100);
        int vertSens = PlayerPrefs.GetInt(VerticalSensitibility, 400);
        sensHori = horiSens;
        sensVert = vertSens;
    }
}
