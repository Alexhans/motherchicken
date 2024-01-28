using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Props : ProjectileEffect
{
    Rigidbody rigidbody;

    public override void OnProjectileHit(ProjectileBehaviour projectileBehaviour)
    {
        Vector3 direction = projectileBehaviour.transform.right * projectileBehaviour.forceOnHit;

        rigidbody.AddForce(direction);
    }

    private void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }
}
