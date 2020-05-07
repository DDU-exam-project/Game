using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float lifeTime = 5f;
    [SerializeField] int damage = 5;

    CharacterScript hitCharacter;
    Animator anim;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        anim.SetTrigger("Hit");
        if (collision.gameObject.GetComponent<CharacterScript>() != null)
        {

            hitCharacter = collision.gameObject.GetComponent<CharacterScript>();


            if (!hitCharacter.WasHit)
            {
                hitCharacter.TakeDamage(damage);
            }
        }
    }
}
