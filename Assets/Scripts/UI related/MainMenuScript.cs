using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject optionsFirstSelected, bacFromOptionsSelected;
    [SerializeField] GameObject optionsPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnOptionsOpenClose()
    {
        optionsPanel.SetActive(!optionsPanel.activeInHierarchy);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

}
