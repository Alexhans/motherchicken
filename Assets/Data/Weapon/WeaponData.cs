using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public float bulletForce = 0.1f;
    public float ammo = 30;
    public float recoilFactor = 1.0f;
    public float coolDown = 0.0f;

    public GameObject bullet;
}