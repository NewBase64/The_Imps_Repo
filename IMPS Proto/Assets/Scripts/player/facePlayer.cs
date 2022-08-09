using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class facePlayer : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(gamemanager.instance.mainCam.transform.position);
    }
}
