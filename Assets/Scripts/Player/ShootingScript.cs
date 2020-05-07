using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class ShootingScript : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject crosshair;

    [SerializeField] float shootingCost = 2;
    [SerializeField] float crossHairDIstance = 1;
    Vector2 aimDir;
    PlayerMovement pM;

    // Start is called before the first frame update
    void Start()
    {
        pM = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Shoot"))
        {
            pM.canMove = false;
        }
        else
        {
            pM.canMove = true;
        }
    }

    void Aim()
    {

    }

    void Shoot()
    {

    }
}
