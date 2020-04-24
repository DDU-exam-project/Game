using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;
    int currentHealth = 0;
    bool isAlive = true;
    bool wasHit = false;

    public int MaxHealth { get => maxHealth; }
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public bool WasHit { get => wasHit; set => wasHit = value; }
    
    
    

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
            
        }
    }

    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        wasHit = true;
    }
}
