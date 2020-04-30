using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : CharacterScript
{
    [SerializeField] float timeBeforeRestart = 1f;
    PlayerMovement movementScript;

    static PlayerScript player;
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
        SceneManager.activeSceneChanged += OnSceneChangeHealthBarUpdate;
        HealthBarScript.InitializeHealthBar(MaxHealth);
        movementScript = GetComponent<PlayerMovement>();
    }

    override
    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        HealthBarScript.UpdateHealthBar(CurrentHealth);
        WasHit = true;
        StartCoroutine(HurtCoroutine());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (SceneManager.GetSceneByName("PauseMenu").isLoaded == false)
            {
                SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.UnloadSceneAsync("PauseMenu");
            }
        }
        if (IsAlive)
        {
            StartCoroutine(gameOverCoroutine());
            SceneManager.LoadSceneAsync("GameOverScreen", LoadSceneMode.Additive);
            movementScript.enabled = false;
        }
    }

    IEnumerator gameOverCoroutine()
    {
        yield return new WaitForSeconds(timeBeforeRestart);
        SceneManager.UnloadSceneAsync("GameOverScreen");
        SceneManager.LoadScene("HubScene");
    }

    void OnSceneChangeHealthBarUpdate(Scene current, Scene next)
    {
        HealthBarScript.UpdateHealthBar(CurrentHealth);
    }
}
