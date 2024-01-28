using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public int Damage;
    public float forceOnHit;

    public void OnTriggerEnter(Collider other)
    {
        ProjectileEffect projectileEffect = other
            .gameObject.
            GetComponent<ProjectileEffect>();

        if (projectileEffect != null)
            projectileEffect.OnProjectileHit(this);
    }
}
