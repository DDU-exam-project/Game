using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {
    List<GameObject> roomGameObjects;
    List<Vector2> takenPositions = new List<Vector2>();
    [SerializeField] int roomSize;
	int size = 20;
	float denistyConstant = 0.1f;
	float straightWalkConstant = 0.3f;
	Room[,] rooms;
	public static List<GameObject> roomsList = new List<GameObject>();
	[SerializeField] List<GameObject> roomTemplates;
	void Start () {

		Generate(); //lays out the actual map
		SetRoomDoors(); //assigns the doors where rooms would connect
		DrawMap();
	}

	void DrawMap()
	{
		foreach (Room room in rooms)
		{
			if (room == null) continue; //skip where there is no room
			
			room.makeTag();
			Vector2 drawPos = room.gridPos;
			drawPos.x *= 1 * roomSize;//aspect ratio of map sprite
			drawPos.y *= 1 * roomSize;
            Vector2 currentPos = room.gridPos;
            foreach (GameObject roomTemplate in roomTemplates)
			{
                if (roomTemplate.CompareTag(room.tag))
                {
					foreach (OnTriggerMoveTo comp in roomTemplate.GetComponentsInChildren<OnTriggerMoveTo>())
					{
						comp.roomPosition = room.gridPos;
					}
					roomTemplate.GetComponent<GridPosition>().gridPos = room.gridPos;
                    GameObject spawned = Instantiate(roomTemplate, drawPos, Quaternion.identity);
					roomsList.Add(spawned);
					if (currentPos != Vector2.zero) {
						spawned.SetActive(false);
					}
                }
            }
		}
    }
	Vector2 Direction(Vector2 LastDirect)
	{
		Vector2 direct = new Vector2(0, 0);
		if (straightWalkConstant > Random.value)
		{
			return LastDirect;
		}
		switch (Random.Range(0, 4))
		{
			case 0:
				direct = new Vector2(0, 1);
				break;
			case 1:
				direct = new Vector2(0, -1);
				break;
			case 2:
				direct = new Vector2(1, 0);
				break;
			case 3:
				direct = new Vector2(-1, 0);
				break;
		}
		if (LastDirect.x + direct.x == 0 & LastDirect.y + direct.y == 0)
		{
			return LastDirect;
		}
		return direct;
	}


	bool containVector2(Vector2 pivot, List<Vector2> list)
	{
		foreach (Vector2 value in list)
		{
			if (value.x == pivot.x & value.y == pivot.y)
			{
				return true;
			}
		}
		return false;
	}


	void Generate()
	{
		int index;
		int x;
		int y;
		rooms = new Room[size * 2, size * 2];
		Vector2 direct = new Vector2(-1, -1);
		List<Vector2> taken = new List<Vector2>(size);
		taken.Add(Vector2.zero);
		rooms[size, size] = new Room(Vector2.zero, 1);
		Vector2 current = Vector2.zero;
		for (int i = 1; i < size; i++)
		{
			if (Mathf.Abs(current.y) > size * denistyConstant | Mathf.Abs(current.y) > size * denistyConstant)
			{
				current = Vector2.zero;
			}
			direct = Direction(direct);
			current += direct;
			if (containVector2(current, taken))
			{
				while (true)
				{
					index = Random.Range(0, i);
					direct = Direction(direct);
					current = taken[index] + direct;
					if (!containVector2(current, taken))
					{
						break;
					}
				}
			}
			x = Mathf.RoundToInt(20 + current.x);
			y = Mathf.RoundToInt(20 + current.y);
			rooms[x, y] = new Room(current, 1);
			taken.Add(current);
		}		
	}

	void SetRoomDoors(){
		for (int x = 0; x < ((size * 2)); x++){
			for (int y = 0; y < ((size * 2)); y++){
				if (rooms[x,y] == null){
					continue;
				}
				Vector2 gridPosition = new Vector2(x,y);
				if (y - 1 < 0){ //check above
					rooms[x,y].doorBot = false;
				}else{
					rooms[x,y].doorBot = (rooms[x,y-1] != null);
				}
				if (y + 1 >= size * 2){ //check bellow
					rooms[x,y].doorTop = false;
				}else{
					rooms[x,y].doorTop = (rooms[x,y+1] != null);
				}
				if (x - 1 < 0){ //check left
					rooms[x,y].doorLeft = false;
				}else{
					rooms[x,y].doorLeft = (rooms[x - 1,y] != null);
				}
				if (x + 1 >= size * 2){ //check right
					rooms[x,y].doorRight = false;
				}else{
					rooms[x,y].doorRight = (rooms[x+1,y] != null);
				}
			}
		}
	}
}
