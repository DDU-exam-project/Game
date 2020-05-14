using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class ChangeSceneScript : MonoBehaviour
{
    [SerializeField] string sceneToGoTo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToGoTo);
            PlayerScript.player.transform.position = Vector3.zero;
        }
    }
}
