using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : CharacterScript
{

    Scene currentScene;
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
        currentScene = SceneManager.GetActiveScene();
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
    }

    void OnSceneChangeHealthBarUpdate(Scene current, Scene next)
    {
        HealthBarScript.UpdateHealthBar(CurrentHealth);
    }
}
