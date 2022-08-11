//CREATED

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scrolling : MonoBehaviour
{
    [SerializeField] RawImage img;
    [SerializeField] float xSpeed;
    [SerializeField] float ySpeed;
    void Update()
    {
        img.uvRect = new Rect(img.uvRect.position + new Vector2(xSpeed, ySpeed) * Time.deltaTime, img.uvRect.size);
    }
}
