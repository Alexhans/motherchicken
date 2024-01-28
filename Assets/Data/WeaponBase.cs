using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponBase : ScriptableObject
{
    public float bulletForce;
    public float ammo;
    public float recoilFactor;
    public float impact;
    public float coolDown;
    // public Vector3 currentPosition = Transform.pos;
    
}