using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    // Enemy target
    public Transform target;

    // Enemy speed
    public float speed = 200f;

    // Minimum attack distance
    public float attackDistance = 10f;

    // Minimum distance to complete way point
    public float nextWaypointDistance = 3;


    Path path;
    int currentWaypoint = 0;
    bool reachEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        // Gets the seeker component
        seeker = GetComponent<Seeker>();
        // Gets the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Start a repeating function
        // 1) The function to call
        // 2) The delay before first call
        // 3) repeat every x seconds
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        
    }


    // Calculate a new path to player
    void UpdatePath() {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    // When path is calculated then update enemy path
    void OnPathComplete(Path p) {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        // If there is no path then return
        if (path == null) 
            return;

        
        //Debug.Log(Vector2.Distance(rb.position, target.position));
        if (Vector2.Distance(rb.position, target.position) <= attackDistance) {
            Debug.LogWarning("ATTACK");
            return;

        }


        // Test if at end of path
        if (currentWaypoint >= path.vectorPath.Count) {
            reachEndOfPath = true;
            return;
        } else {
            reachEndOfPath = false;
        }

        // Calculate a normalized vector ind the direaction of path
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        // Calculate the force to add to enemy
        Vector2 force = direction * speed * Time.fixedDeltaTime;

        // Addeds force to enemy
        rb.AddForce(force);

        // Calculate distance between enemy and next way point
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        // If close enough to way point then proceed to next way point
        if (distance < nextWaypointDistance) {
            currentWaypoint++;
        }
    }
}
