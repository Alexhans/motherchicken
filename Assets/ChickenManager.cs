using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class ChickenManager : MonoBehaviour
{

    [SerializeField]
    private float health = 100.0f;

    [SerializeField]
    private GameObject selectedBullet;

    [SerializeField]
    private Transform gunPoint;

    [SerializeField]
    private Rigidbody myRigidBody;

    [SerializeField]
    private Vector3 aimVector;

    // Recoil
    [SerializeField]
    float bulletRecoilFactor = 0.3f;

    [SerializeField]
    float bulletForce = 20.0f;

    [SerializeField]
    float rayDistanceDebugDraw = 10.0f;

    float chickSpeed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello: " + gameObject.name);
        if(selectedBullet == null)
        {
            Debug.LogError("Empty");
        }
    }

    float speed = 1.0f;
    void FixedUpdate()
    {
        // Generate a plane that intersects the transform's position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.forward, transform.position);

        // Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           
        // Determine the point where the cursor ray intersects the plane.
        // This will be the point that the object must look towards to be looking at the mouse.
        // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
        //   then find the point along that ray that meets that distance.  This will be the point
        //   to look at.
        
        float hitdist = 0.0f;
        
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);

            Debug.DrawRay(gunPoint.transform.position, Vector3.Normalize(targetPoint - gunPoint.transform.position) * 5.0f);

            aimVector = Vector3.Normalize(targetPoint - gunPoint.transform.position);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //aimVector = transform.right;
        Debug.DrawRay(transform.position, aimVector * 1.0f, Color.green);
        
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 some = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Debug.Log(ray);
        Debug.DrawRay(transform.position, ray.direction, Color.red);
        if (Input.GetKeyDown("t"))
        {
            Shoot();
        }
       
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (myRigidBody)
            {
                Debug.Log("Left");
                Vector3 forward = transform.TransformDirection(Vector3.up) * 10;
                myRigidBody.AddForce(transform.right * -chickSpeed, ForceMode.Impulse);
                transform.rotation = Quaternion.LookRotation(transform.right * -chickSpeed);
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (myRigidBody)
            {
                Debug.Log("Right");
                Vector3 forward = transform.TransformDirection(Vector3.up) * 10;
                myRigidBody.AddForce(transform.right * chickSpeed, ForceMode.Impulse);
                transform.rotation = Quaternion.LookRotation(transform.right * chickSpeed);
            }
        }
    }

    public void Shoot()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * rayDistanceDebugDraw;
        Debug.DrawRay(transform.position, forward, Color.green);
        Debug.Log("Bang!");
        if(selectedBullet) { 
            GameObject bullet = Instantiate<GameObject>(selectedBullet.gameObject, gunPoint.position, gunPoint.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(aimVector * bulletForce, ForceMode.Impulse);

            // Recoil
            myRigidBody.GetComponent<Rigidbody>().AddForce(-(aimVector * bulletForce) * bulletRecoilFactor, ForceMode.Impulse);
        }
        
    }
}
