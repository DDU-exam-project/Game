using Cinemachine;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam;

    static CameraScript instance;
    
    public static CameraScript Instance { get => instance; }
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
    }

    
}
