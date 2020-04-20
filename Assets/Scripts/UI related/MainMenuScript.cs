using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject optionsFirstSelected, backFromOptionsSelected;
    [SerializeField] GameObject optionsPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
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

    public void CloseGame()
    {
        Application.Quit();
    }

}
