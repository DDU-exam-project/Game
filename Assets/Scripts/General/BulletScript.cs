using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Vector2 velocity;   
    public GameObject player;


    [SerializeField] Vector2 rayOffset;
    [SerializeField] float lifeTime = 5f;
    [SerializeField] int damage = 5;

    
    Animator anim;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
        anim = GetComponent<Animator>();
        
    }
    private void Update()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + velocity * Time.deltaTime;

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition + rayOffset, newPosition + rayOffset);

        foreach (RaycastHit2D hit in hits)
        {
            GameObject other = hit.collider.gameObject;

            if (other != player)
            {
                velocity = Vector2.zero;
                anim.SetTrigger("Hit");
                if (other.CompareTag("Enemy"))
                {
                    other.GetComponent<EnemyScript>().TakeDamage(damage);
                    break;
                }
            }
        }

        transform.position = newPosition;
        
    }
}
