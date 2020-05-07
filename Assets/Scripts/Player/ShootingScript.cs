using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class ShootingScript : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject crosshair;

    [SerializeField] int shootingCost = 2;
    [SerializeField] float crosshairDistance = 1f;
    [SerializeField] float bulletSpeed = 1f;
    [SerializeField] float timeBetweenShots = 1f;

    float cooldown = 0;

    Vector2 crosshairDir;
    PlayerMovement pM;

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
        
        if (Input.GetButton("Shoot"))
        {
            pM.canMove = false;
            pM.animator.SetBool("Aiming", true);
            pM.animator.SetFloat("ShootState", 0f);
            crosshair.SetActive(true);
            Aim();           
        }
        else if (Input.GetButtonUp("Shoot"))
        {
            pM.canMove = true;
            crosshair.SetActive(false);
            pM.animator.SetBool("Aiming", false);
            Shoot();
        }
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
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
        
        if (cooldown <= 0)
        {
            PlayerScript.player.TakeDamage(shootingCost);

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            BulletScript bulletScript = bullet.GetComponent<BulletScript>();

            bulletScript.velocity = shootingDirection * bulletSpeed;
            bulletScript.player = gameObject;
            bullet.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
            pM.animator.SetFloat("ShootState", 1f);
            cooldown = timeBetweenShots;
        }
    }
}
