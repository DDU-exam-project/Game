using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerMoveTo : MonoBehaviour
{
    string tag;
    string newDoor;
    public Vector2 roomPosition;
    Vector2 temp;

    void FixedUpdate()
    {
        tag = gameObject.tag;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GridPosition[] GridPositions = Object.FindObjectsOfType<GridPosition>();
        GameObject[] rooms = new GameObject[GridPositions.Length];
        for (int i = 0; i < rooms.Length; i++)
        {
            rooms[i] = GridPositions[i].gameObject;
        }
        if (collision.CompareTag("Player") & PlayerScript.player.canTeleport)
        {
            switch (tag)
            {
                case "R":
                    newDoor = "L";
                    temp = new Vector2(1, 0);
                    break;
                case "L":
                    newDoor = "R";
                    temp = new Vector2(-1, 0);
                    break;
                case "T":
                    newDoor = "B";
                    temp = new Vector2(0, 1);
                    break;
                case "B":
                    newDoor = "T";
                    temp = new Vector2(0, -1);
                    break;
                default:
                    throw new System.ArgumentException("The door didn't have a tag that is R, L, T or B", "Invalid Tag");
            }
            foreach (GameObject room in rooms)
            {
                if (room.GetComponent<GridPosition>().gridPos == roomPosition + temp)
                {
                    Debug.Log(roomPosition);
                    Debug.Log(roomPosition + temp);
                    Debug.Log(newDoor);
                    PlayerScript.player.teleportResetTimer();
                    collision.gameObject.transform.position = room.transform.Find(newDoor).position;
                }
            }
        }
    }
}
