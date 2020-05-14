using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject pauseFirstSelected, optionsFirstSelected, backFromOptionsSelected;
    [SerializeField] GameObject optionsPanel;

    private void OnEnable()
    {
        Time.timeScale = 0f;
        //clear selection
        EventSystem.current.SetSelectedGameObject(null);
        //Make new selection
        EventSystem.current.SetSelectedGameObject(pauseFirstSelected);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Unpause();
        }
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1f;
        Destroy(PlayerScript.player.gameObject);
        Destroy(CameraScript.Instance.gameObject);
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    public void OnOptionsOpenClose()
    {
        optionsPanel.SetActive(!optionsPanel.activeInHierarchy);

        //clear selection
        EventSystem.current.SetSelectedGameObject(null);
        //set new selction
        if (optionsPanel.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(optionsFirstSelected);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(backFromOptionsSelected);
        }
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync("PauseMenu");
    }
}
