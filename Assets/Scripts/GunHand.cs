using UnityEngine;

public class GunHand : MonoBehaviour
{
    public LayerMask layer;
    public Transform muzzle;
    public int bulletAmount;
    public float weaponCooldown;
    public WeaponData defaultWeapon;

    private WeaponData weapon;
    private Camera mainCamera;
    private Vector3 mousePosition;
    private Rigidbody myBodyRigidBody;
    private ChickenManager chickenManager;

    public WeaponData Weapon
    {
        get { return weapon; }
        set {
            weapon = value;
            bulletAmount = weapon.weaponAmmo;
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate<GameObject>(
            weapon.bullet,
            muzzle.position,
            transform.rotation
        );

        bullet.transform.forward = transform.forward;
        bullet.GetComponent<Projectile>().forceOnHit += weapon.bulletAdditionalForce;
        bullet.GetComponent<Rigidbody>().AddForce(transform.right * weapon.bulletSpeed, ForceMode.VelocityChange);

        myBodyRigidBody.GetComponent<Rigidbody>().AddForce(-transform.right * weapon.weaponRecoilFactor, ForceMode.Impulse);
        chickenManager.BecomeStressed(weapon.stress);

        bulletAmount--;
        weaponCooldown = weapon.weaponCoolDown;
    }

    private void Awake()
    {
        mainCamera = Camera.main;
        myBodyRigidBody = transform.parent.gameObject.GetComponent<Rigidbody>();
        chickenManager = transform.parent.gameObject.GetComponent<ChickenManager>();
        Weapon = defaultWeapon;
    }

    // Update is called once per frame
    void Update()
    {
        Ray mousePositionRay = mainCamera.ScreenPointToRay(Input.mousePosition); //Input.mousePosition;
        if (Physics.Raycast(mousePositionRay, out RaycastHit raycasthit, layer))
        {
            mousePosition = new Vector3(raycasthit.point.x, raycasthit.point.y, 0);
            Vector3 rotation = (mousePosition - transform.position);          
            float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            chickenManager.RotateBody(angle);

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (weaponCooldown != 0)
            if (weaponCooldown > 0)
                weaponCooldown -= Time.deltaTime;
            else
                weaponCooldown = 0;

        if (chickenManager.StunTime <= 0)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                weaponCooldown = weapon.stunTime;
                chickenManager.StunTime = weapon.stunTime;
            }
            else
            {
                if ((weaponCooldown == 0) && (bulletAmount != 0) && Input.GetKey(KeyCode.Mouse0))
                    Shoot();
            }
        }
    }
}
