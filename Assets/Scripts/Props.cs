using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Props : MonoBehaviour
{
    Rigidbody rigidbody;

    private void OnTriggerEnter(Collider other)
    {
        Projectile projectile = other.gameObject.GetComponent<Projectile>();

        if (projectile != null)
        {
            Vector3 direction = projectile.transform.right * projectile.forceOnHit;

            rigidbody.AddForce(direction);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();

        if (projectile != null)
        {
            Vector3 direction = projectile.transform.right * projectile.forceOnHit;

            rigidbody.AddForce(direction);
        }
    }

    private void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }
}
