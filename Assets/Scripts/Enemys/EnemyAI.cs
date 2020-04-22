using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3;

    Path path;
    int currentWaypoint = 0;
    bool reachEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        
    }

    void UpdatePath() {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p) {
        Debug.Log("A path was calculated. Did it fail with an error? " + p.error);
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (path == null) 
            return;

        if (currentWaypoint >= path.vectorPath.Count) {
            reachEndOfPath = true;
            return;
        } else {
            reachEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.fixedDeltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance) {
            currentWaypoint++;
        }
    }
}
