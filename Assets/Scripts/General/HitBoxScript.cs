using UnityEngine;

public class HitBoxScript : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] bool isPlayerHit = false;
    [SerializeField] int lifesteal = 1;

    CharacterScript hitCharacter;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterScript>() != null)
        {

            hitCharacter = collision.gameObject.GetComponent<CharacterScript>();


            if (!hitCharacter.WasHit)
            {
                hitCharacter.TakeDamage(damage);
                if (isPlayerHit)
                {
                    PlayerScript.player.LifeSteal(lifesteal);
                }
            }
        }
    }
}
