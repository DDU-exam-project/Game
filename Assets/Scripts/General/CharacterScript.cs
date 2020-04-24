using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;
    int currentHealth = 0;

    public int MaxHealth { get => maxHealth; }
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    
    bool isAlive = true;

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
    }
}
