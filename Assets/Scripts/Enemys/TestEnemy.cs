using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float minDist = 2f;

    public Rigidbody2D rb;

    GameObject player;

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if( Vector2.Distance(rb.transform.position, player.transform.position) >= minDist) {
            rb.transform.position = Vector2.MoveTowards(
                rb.transform.position,
                player.transform.position,
                moveSpeed * Time.deltaTime
            );
        }
    }

    void FixedUpdate() 
    {
        // Movment
        //rb.MovePosition(rb.position + player.position * moveSpeed * Time.fixedDeltaTime);
    }
}
