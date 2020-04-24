using UnityEngine;

public class HitBoxScript : MonoBehaviour
{
    [SerializeField] int damage = 1;

    CharacterScript hitCharacter;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
