using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerMoveTo : MonoBehaviour
{
    public Vector2 newPosition;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.position = newPosition;
        }
    }
}
