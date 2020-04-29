using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerMoveTo : MonoBehaviour
{
    string tag;
    public Vector2 newPosition;
    public Vector2 roomPosition;

    void FixedUpdate()
    {
        tag = gameObject.tag;
        Debug.Log(roomPosition);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (tag)
            {
                case "R": break;
                case "L": break;
                case "T": break;
                case "D": break;
                default:
                    throw new System.ArgumentException("The door didn't have a tag that is R, L, T or D", "Invalid Tag");
            }
            collision.gameObject.transform.position = newPosition;
        }
    }
}
