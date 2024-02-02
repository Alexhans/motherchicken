using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public GameObject bullet;
    public float bulletForce = 0.1f;
    public float bulletSpeed = 1;

    public GameObject weapon;
    public int weaponAmmo = 30;
    public float weaponRecoilFactor = 1.0f;
    public float weaponCoolDown = 0.0f;
}