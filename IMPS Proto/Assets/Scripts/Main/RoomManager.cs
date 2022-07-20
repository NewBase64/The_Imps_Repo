using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomManager : MonoBehaviour
{
    [HideInInspector] public static RoomManager instance;

    public int rooms;

    public int playerRoom;

    List<int> enemiesInRoom = new List<int>();
    List<int> enemiesKilledInRoom = new List<int>();

    public TMP_Text enemyDead;
    public TMP_Text enemyTotal;
    public TMP_Text currentRoom;

    public GameObject entrance;

    void Awake()
    {
        instance = this;
    }

    public void openDoor()
    {
        if (GameObject.Find("Door" + playerRoom))
        {
            GameObject door = GameObject.Find("Door" + playerRoom);
            door.SetActive(false);
        }
    }

    public void checkEnemiesKilledOnRoom(int room)
    {
        enemiesKilledInRoom[room - 1]++;
        enemyDead.text = enemiesKilledInRoom[room - 1].ToString("F0");
        enemiesInRoom[room - 1]--;

        if (enemiesInRoom[room - 1] == 0)
        {
            rooms--;
            openDoor();
        }

        if (rooms == 0)
        {
            gamemanager.instance.menuCurrentlyOpen = gamemanager.instance.winGameMenu;
            gamemanager.instance.CurrCrosshair.SetActive(false);
            gamemanager.instance.menuCurrentlyOpen.SetActive(true);
            gamemanager.instance.gameOver = true;
            gamemanager.instance.ConLockCursor();
        }


    }

    public void updateEnemyNumberOnRoom(int room)
    {
        enemiesInRoom[room - 1]++;
        enemyTotal.text = enemiesInRoom[room - 1].ToString("F0");
    }

    public void updateNumberOfRooms()
    {
        rooms++;
        enemiesInRoom.Add(0);
        enemiesKilledInRoom.Add(0);
    }

    public void updateObjectiveUi(int room)
    {
        currentRoom.text = playerRoom.ToString("F0");
        enemyDead.text = enemiesKilledInRoom[room - 1].ToString("F0");
        enemyTotal.text = enemiesInRoom[room - 1].ToString("F0");
    }
}
