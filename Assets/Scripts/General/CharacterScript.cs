using System.Collections;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;
    [SerializeField] float hurtTime = 0.3f;
    int currentHealth = 0;
    bool isAlive = true;
    bool wasHit = false;

    public int MaxHealth { get => maxHealth; }
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public bool WasHit { get => wasHit; set => wasHit = value; }
    public float HurtTime { get => hurtTime; set => hurtTime = value; }
    
    
    

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }
  

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            isAlive = false;
            this.enabled = false;
        }
    }

    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        wasHit = true;
        StartCoroutine(HurtCoroutine());
    }

    public IEnumerator HurtCoroutine()
    {
        yield return new WaitForSeconds(hurtTime);
        wasHit = false;
    }
}
