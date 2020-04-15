using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour {

    public Rigidbody2D rb;
    public GameObject player;
    public float moveSpeed = 5f;
    public float minDist = 2f;
    public float maxDist = 2f;


    private bool spottedPlayer = false;

    // Update is called once per frame
    void Update() {

        // Find player object
        //player = GameObject.FindGameObjectWithTag("Player");

        // If player is whitind maxDist, then player is spotted
        if (!spottedPlayer & Vector2.Distance(rb.transform.position, player.transform.position) <= maxDist) spottedPlayer = true;

        // If player is spotted and is not whitind minDist, then move towards player
        if (spottedPlayer & Vector2.Distance(rb.transform.position, player.transform.position) >= minDist) {

            Vector2 direction = player.transform.position - rb.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;

            rb.transform.position = Vector2.MoveTowards(
                rb.transform.position,
                player.transform.position,
                moveSpeed * Time.deltaTime
            );
        }
    }

    void FixedUpdate() {
        // Movment
        //rb.MovePosition(rb.position + player.position * moveSpeed * Time.fixedDeltaTime);
    }
}
