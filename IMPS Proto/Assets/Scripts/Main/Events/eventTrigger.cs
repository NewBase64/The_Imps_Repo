using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventTrigger : MonoBehaviour
{
    [SerializeField] int causedEvent;
    GameObject me;

    void Start()
    {
        me = this.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gamemanager.instance.CauseEvent(causedEvent);
            me.SetActive(false);
        }
    }
}
