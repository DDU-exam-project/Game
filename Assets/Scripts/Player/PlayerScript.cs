public class PlayerScript : CharacterScript
{
    PlayerMovement movementScript;
    public bool canTeleport = true;

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
    
    public void teleportResetTimer()
    {
        canTeleport = false;
        Invoke("teleportReset", 5.0f);
    }

    private void teleportReset()
    {
        canTeleport = true;
    }

    override
    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        WasHit = true;
        if (CurrentHealth <= 0)
        {
            movementScript.animator.SetTrigger("Die");
            movementScript.enabled = false;
            GameManager.Instance.EndGame();
            return;
        }
        
        StartCoroutine(HurtCoroutine());
    }

    public void LifeSteal(int amount)
    {
        if (CurrentHealth + amount <= MaxHealth)
        {
            CurrentHealth += amount;
        }       
    }
}
