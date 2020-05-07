using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class ShootingScript : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject crosshair;

    [SerializeField] float shootingCost = 2f;
    [SerializeField] float crosshairDistance = 1f;
    [SerializeField] float bulletSpeed = 1f;

    Vector2 crosshairDir;
    PlayerMovement pM;
    bool endOfAiming;

    // Start is called before the first frame update
    void Start()
    {
        pM = GetComponent<PlayerMovement>();
        //crosshair.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        crosshairDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        endOfAiming = Input.GetButtonUp("Shoot");
        Aim();
        Shoot();
        /*if (Input.GetButton("Shoot"))
        {
            pM.canMove = false;
            pM.animator.SetBool("Aiming", true);
            crosshair.SetActive(true);
            Aim();
        }
        else
        {
            pM.canMove = true;
            crosshair.SetActive(false);
        }*/
    }

    void Aim()
    {
        if (crosshairDir != Vector2.zero)
        {
            crosshair.transform.localPosition = (crosshairDir * crosshairDistance).normalized;
            pM.animator.SetFloat("AimX", crosshairDir.x);
            pM.animator.SetFloat("AimY", crosshairDir.y);
        }
    }

    void Shoot()
    {
        Vector2 shootingDirection = crosshair.transform.localPosition;
        shootingDirection.Normalize();
        if (endOfAiming)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = shootingDirection * bulletSpeed;
        }
    }
}
