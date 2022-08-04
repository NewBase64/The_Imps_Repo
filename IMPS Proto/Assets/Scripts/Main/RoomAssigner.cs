using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAssigner : MonoBehaviour
{
    private void Awake()
    {
        RoomManager.instance.updateNumberOfRooms();
    }
}
