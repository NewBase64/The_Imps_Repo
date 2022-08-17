using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomManager : MonoBehaviour
{
    [HideInInspector] public static RoomManager instance;

    public int rooms;

    public int playerRoom = 1;

    List<int> enemiesInRoom = new List<int>();
    List<int> enemiesKilledInRoom = new List<int>();

    public TMP_Text enemyDead;
    public TMP_Text enemyTotal;
    public TMP_Text currentRoom;

    public GameObject checkpoint;

    public bool LastLevel = false;
    void Awake()
    {
        instance = this;
        checkpoint = GameObject.Find("Checkpoint1");
    }

    public void openDoor()
    {
        if (GameObject.Find("Door" + playerRoom) && rooms != 0)
        {
            GameObject door = GameObject.Find("Door" + playerRoom);
            door.SetActive(false);
            playerRoom++;
            checkpoint = GameObject.Find("Checkpoint" + playerRoom);
            updateObjectiveUi();
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

        if (rooms == 0 && !LastLevel)
        {
            gamemanager.instance.menuCurrentlyOpen = gamemanager.instance.winGameMenu;
            gamemanager.instance.menuCurrentlyOpen.SetActive(true);
            gamemanager.instance.gameOver = true;
            gamemanager.instance.ConLockCursor();
        }
        else if (rooms == 0 && LastLevel)
        {
            currentRoom.text = "";
            enemyDead.text = "???";
            enemyTotal.text = "???";
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

    public void updateObjectiveUi()
    {
        //Debug.Log(playerRoom);
        currentRoom.text = playerRoom.ToString("F0");
        enemyDead.text = enemiesKilledInRoom[playerRoom - 1].ToString("F0");
        enemyTotal.text = enemiesInRoom[playerRoom - 1].ToString("F0");
    }
}
