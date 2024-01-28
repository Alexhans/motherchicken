using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyBulletOnCollision : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(DestroyAfterSeconds(0));
    }

    IEnumerator DestroyAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
    
}
