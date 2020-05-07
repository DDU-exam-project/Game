using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Animator animator;

    public bool canMove = true;

    Vector2 movement;
    Rigidbody2D rb;


    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetButtonDown("Fire3") && canMove)
        {
            canMove = false;
            animator.SetTrigger("Attack");            
        }


        if (movement.sqrMagnitude != 0 && canMove)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }

    void FixedUpdate() 
    {
        // Movment
        if (canMove)
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }       

    }
}
