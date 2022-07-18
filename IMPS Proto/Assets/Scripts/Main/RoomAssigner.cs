using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAssigner : MonoBehaviour
{
    private void Awake()
    {
        RoomManager.instance.updateNumberOfRooms();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            RoomManager.instance.playerRoom++;
            RoomManager.instance.entrance = gameObject;
            gameObject.SetActive(false);
            RoomManager.instance.updateObjectiveUi(RoomManager.instance.playerRoom);
        }    
    }


}
