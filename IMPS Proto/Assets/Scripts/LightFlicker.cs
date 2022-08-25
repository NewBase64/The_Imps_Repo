using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public bool isFlickering = true;
    public float timeDelay = 1f;
    public Light[] lights;

    // Update is called once per frame

    public void StartFlicker()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].type = LightType.Point;
            lights[i].color = Color.red;
            lights[i].intensity = 20;
            lights[i].transform.position -= new Vector3(0, 15, 0);
            lights[i].range = 40;
            isFlickering = false;
        }
    }
    void Update()
    {
        if (isFlickering == false)
        {
            StartCoroutine(FlickeringLight());
        }
    }

    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].enabled = false;
        }
        yield return new WaitForSeconds(timeDelay);
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].enabled = true;
        }
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
