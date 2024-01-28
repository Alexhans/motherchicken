using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookManager : ProjectileEffect
{
    Rigidbody rb;

    [SerializeField]
    string followObjectDefaultTag = "Player";

    [SerializeField]
    string followObjectDefaultFindString = "ChickenRot";

    [SerializeField]
    GameObject followObject;

    [SerializeField]
    private float speed = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        followObject = GameObject.Find(followObjectDefaultFindString);
        if (followObject == null)
        {
            Debug.Log("Follow object NOT found");
        } else {
            Debug.Log("Follow object found");
        }
    }

    void Update()
    {
        Vector3 followObjectPosition = followObject.transform.position;
        if (followObjectPosition.x > transform.position.x)
        {
            rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
        }
        else if (followObjectPosition.x < transform.position.x)
        {
            rb.velocity = new Vector3(-speed, rb.velocity.y, rb.velocity.z);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // if (collision.gameObject.tag.Equals(followObjectDefaultTag))
        // {
        //     Destroy(gameObject);
        // }
    }

    public override void OnProjectileHit(ProjectileBehaviour projectileBehaviour)
    {
        Debug.Log("<color=green>Alex, please implement the effect here.</color>");
    }
}
