using UnityEngine;

public class OnTriggerMoveTo : MonoBehaviour
{
    string tag;
    string newDoor;
    public Vector2 roomPosition;
    Vector2 temp;
    GameObject currentRoom;

    void OnEnable() {
        currentRoom = gameObject.transform.parent.gameObject;
    }

    void FixedUpdate()
    {
        tag = gameObject.tag;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
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
            foreach (GameObject room in LevelGeneration.roomsList)
            {

                if (room.GetComponent<GridPosition>().gridPos == roomPosition + temp)
                {
                    currentRoom.SetActive(false);
                    room.SetActive(true);
                    PlayerScript.player.teleportResetTimer();
                    collision.gameObject.transform.position = room.transform.Find(newDoor).position;
                    break;
                }
            }
        }
    }
}
