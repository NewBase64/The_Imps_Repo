using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{

    //[SerializeField] Animator transition;
    [SerializeField] GameObject platform;
    public void OnTriggerStay()
    {
        //gamemanager.instance.player.transform.parent = transform;
        platform.transform.position += platform.transform.up * Time.deltaTime;
    }

    //private void Start()
    //{
    //    transition.SetTrigger("Start");
    //}

}
