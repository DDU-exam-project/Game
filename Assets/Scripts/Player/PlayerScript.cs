using UnityEngine;

public class PlayerScript : CharacterScript
{
    PlayerMovement movementScript;

    public static PlayerScript player;
    private void Awake()
    {
        if (player == null)
        {
            DontDestroyOnLoad(gameObject);
            player = this;
        }
        else
        {
            Destroy(gameObject);
        }           
    }

    private void Start()
    {
        movementScript = GetComponent<PlayerMovement>();
    }
    

    override
    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        WasHit = true;
        if (CurrentHealth <= 0)
        {
            movementScript.animator.SetTrigger("Die");
            GameManager.Instance.EndGame();
            return;
        }
        
        StartCoroutine(HurtCoroutine());
    }

    
}
