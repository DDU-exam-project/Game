using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class ShootingScript : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject crosshair;

    [SerializeField] float shootingCost = 2;
    [SerializeField] float crosshairDistance = 1;
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
        aimDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
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
        if (aimDir != Vector2.zero)
        {
            crosshair.transform.localPosition = aimDir * crosshairDistance;
        }
    }

    void Shoot()
    {

    }
}
