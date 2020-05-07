using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class ShootingScript : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject crosshair;

    [SerializeField] int shootingCost = 2;
    [SerializeField] float crosshairDistance = 1f;
    [SerializeField] float bulletSpeed = 1f;

    Vector2 crosshairDir;
    PlayerMovement pM;
    bool endOfAiming;
    bool isAiming;

    // Start is called before the first frame update
    void Start()
    {
        pM = GetComponent<PlayerMovement>();
        crosshair.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        crosshairDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        endOfAiming = Input.GetButtonUp("Shoot");
        Shoot();
        if (Input.GetButton("Shoot"))
        {
            pM.canMove = false;
            pM.animator.SetBool("Aiming", true);
            pM.animator.SetFloat("ShootState", 0f);
            crosshair.SetActive(true);
            Aim();           
        }
        else
        {
            pM.canMove = true;
            crosshair.SetActive(false);
            pM.animator.SetBool("Aiming", false);
        }
    }

    void Aim()
    {
        if (crosshairDir != Vector2.zero)
        {
            crosshair.transform.localPosition = crosshairDir * crosshairDistance;
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
            pM.animator.SetFloat("ShootState", 1f);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            BulletScript bulletScript = bullet.GetComponent<BulletScript>();

            bulletScript.velocity = shootingDirection * bulletSpeed;
            bulletScript.player = gameObject;
            bullet.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
        }
    }
}
