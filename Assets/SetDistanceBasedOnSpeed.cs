using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDistanceBasedOnSpeed : MonoBehaviour
{

    
    [SerializeField]
    private float distanceScale;

    [SerializeField]
    private float maxDistance;

    [SerializeField]
    private float minDistance;

    [SerializeField]
    private float lerpSpeed = 1;

    [SerializeField]
    private GameObject chicken;

    [SerializeField]
    private GameObject advancedTarget;
    private bool something;

    private void Start()
    {
        // TODO: Assign player on start / Switch
        AssignPlayer();
    }

    public void AssignPlayer()
    {
        //shipController = source.gameObject.GetComponent<ShipController2>();
        //auto something = chicken.GetComponent < )> ();
        

    }

    private void FixedUpdate()
    {
        //
       /* var velocity = gameObject.rb.velocity;
        var motionVector = velocity.normalized;
        var motionMagnitude = velocity.magnitude;
        var targetPosition = source.position + (Vector3)(motionVector *
                                                         (Mathf.Max(minDistance,
                                                         (Mathf.Min(maxDistance, motionMagnitude) * distanceScale)
                                                         )));

        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.fixedDeltaTime);
        //*/
    
}
}
