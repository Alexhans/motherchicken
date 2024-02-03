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

    public WeaponData Weapon
    {
        get { return weapon; }
        set {
            weapon = value;
            bulletAmount = weapon.weaponAmmo;
        }
    }


    private void Awake()
    {
        mainCamera = Camera.main;
        myBodyRigidBody = transform.parent.gameObject.GetComponent<Rigidbody>();
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
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (weaponCooldown != 0)
            if (weaponCooldown > 0)
                weaponCooldown -= Time.deltaTime;
            else
                weaponCooldown = 0;

        if ((weaponCooldown == 0) && (bulletAmount != 0) && Input.GetKey(KeyCode.Mouse0))
        {
            GameObject bullet = Instantiate<GameObject>(
                weapon.bullet,
                muzzle.position, 
                transform.rotation
            );

            bullet.transform.forward = transform.forward;

            bullet.GetComponent<Rigidbody>().AddForce(transform.right * weapon.bulletSpeed, ForceMode.Impulse);

            myBodyRigidBody.GetComponent<Rigidbody>().AddForce(-(transform.right * weapon.bulletForce) * weapon.weaponRecoilFactor, ForceMode.Impulse);

            bulletAmount--;
            weaponCooldown = weapon.weaponCoolDown;
        }
    }
}
