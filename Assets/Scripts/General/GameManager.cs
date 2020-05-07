using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] float timeBeforeRestart = 1f;

    static GameManager instance;

    public static GameManager Instance { get => instance; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.LoadSceneAsync("HealthBarUI", LoadSceneMode.Additive);
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

    public void EndGame()
    {
        StartCoroutine(GameOverAsyncLoad());

        SceneManager.UnloadSceneAsync("HealthBarUI");
        Invoke("RestartGame", timeBeforeRestart);
    }

    void RestartGame()
    {
        Destroy(PlayerScript.player.gameObject);
        
        SceneManager.LoadScene("HubScene");
        SceneManager.LoadSceneAsync("HealthBarUI", LoadSceneMode.Additive);
    }
    
    IEnumerator GameOverAsyncLoad()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameOverScreen");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Destroy(CameraScript.Instance.gameObject);
    }
}
