using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class ShootingScript : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject crosshair;

    [SerializeField] float shootingCost = 2;
    [SerializeField] float crosshairDistance = 1;
    Vector2 crosshairDir;
    PlayerMovement pM;

    // Start is called before the first frame update
    void Start()
    {
        pM = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        crosshairDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetButton("Shoot"))
        {
            pM.canMove = false;
            pM.animator.SetBool("Aiming", true);
            Aim();
        }
        else
        {
            pM.canMove = true;

        }
    }

    void Aim()
    {
        if (crosshairDir != Vector2.zero)
        {
            crosshair.transform.localPosition = crosshairDir * crosshairDistance;

        }
    }

    void Shoot()
    {
        Vector2 shootingDirection = crosshair.transform.localPosition;
        shootingDirection.Normalize();
    }
}
