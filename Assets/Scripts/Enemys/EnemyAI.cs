using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    // Enemy target
    public Transform target;

    // Enemy animator
    public Animator animator;

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

    Vector2 force;
    Vector2 direction;

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
        //StartCoroutine(UpdatePath());
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

    void Update() {

        //if (force.x > 0) {
        //    animator.SetFloat("Horizontal", force.x);
        //} else {
        //    animator.SetFloat("Horizontal", rb.velocity.x);
        //}

        //if (force.y > 0) {
        //    animator.SetFloat("Horizontal", force.y);
        //} else {
        //    animator.SetFloat("Horizontal", rb.velocity.y);
        //}

        //animator.SetFloat("Horizontal", rb.velocity.x);
        //animator.SetFloat("Vertical", rb.velocity.y);
        //animator.SetFloat("Speed", 1);
        //animator.SetFloat("Horizontal", direction.x);
        //animator.SetFloat("Vertical", direction.y);
		animator.SetFloat("Horizontal", rb.velocity.x);
		animator.SetFloat("Vertical", rb.velocity.y);
		animator.SetFloat("Speed", rb.velocity.sqrMagnitude);

    }
    void FixedUpdate()
    {
        // If there is no path then return
        if (path == null) 
            return;

        
        // Test if enemy is within attack distance
        if (Vector2.Distance(rb.position, target.position) <= attackDistance) {
            //Debug.LogWarning("ATTACK");
            //Debug.Log(Vector2.Distance(rb.position, target.position));
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
        direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        // Calculate the force to add to enemy
        //Vector2 force = direction * speed * Time.fixedDeltaTime;
        force = direction * speed * Time.deltaTime;

        // Addeds force to enemy
        //rb.AddForce(force);
        rb.velocity = force;

        // Calculate distance between enemy and next way point
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        // If close enough to way point then proceed to next way point
        if (distance < nextWaypointDistance) {
            currentWaypoint++;
        }
    }
}
