using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public GameObject bullet;
    public float bulletAdditionalForce = 0f;
    public float bulletSpeed = 1;

    public GameObject weapon;
    public int weaponAmmo = 30;
    public float weaponRecoilFactor = 1.0f;
    public float weaponCoolDown = 0.0f;
    public float stunTime = 0;
    public float stress = 0.1f;
}