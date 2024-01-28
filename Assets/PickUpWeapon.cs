using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{

    public delegate void PickUpDropAction();
    public static event PickUpDropAction OnPickUp;
    [SerializeField]
    WeaponData weapon;

    [SerializeField]
    float explosionForce = 10.0f;

    [SerializeField]
    float explosionRadius = 1.0f;

    [SerializeField]
    float dropForce = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(string.Format("collide with: {0}", other.gameObject.name));

        ChickenManager chicken = other.gameObject.GetComponent<ChickenManager>();

        if (chicken != null)
        {
            
            Debug.Log(string.Format("Boom!", other.gameObject.name));
            Rigidbody rigidBody = other.GetComponent<Rigidbody>();
            rigidBody.GetComponent<Rigidbody>().AddForce(Vector2.up * dropForce, ForceMode.Impulse);
            // Egg
            //Instantiate<GameObject>(egg, eggPoint.position, Quaternion.identity);
            Destroy(gameObject);
            

        }
        
    }
}
