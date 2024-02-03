using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class ChickenManager : MonoBehaviour
{

    // Quick and dirty events
    public delegate void SurviveAction();
    public static event SurviveAction OnSurvive;

    public delegate void WinLevelAction();
    public static event WinLevelAction OnWinLevel;

    public delegate void LevelStartAction();
    public static event LevelStartAction OnLevelStart;
    
    public delegate void HatchEggAction();
    public static event HatchEggAction OnHatchEgg;
    // attrs
    // attrs
    [SerializeField]
    GameObject egg;

    [SerializeField]
    private float health = 100.0f;

    [SerializeField]
    private float stress = 5.0f;
    
    [SerializeField]
    private float stressToHatch = 10.0f;

    [SerializeField]
    private Transform eggPoint;

    [SerializeField]
    private Rigidbody myRigidBody;

    [SerializeField]
    float eggForce = 4.0f;

    [SerializeField]
    float chickSpeed = 2.0f;

    [SerializeField]
    float timeToSurviveInitial = 100.0f;

    [SerializeField]
    float timeToSurviveLeft = 99.0f;

    float speed = 1.0f;

    private float stunned;

    public float StunTime
    {
        get { return stunned; }
        set { stunned += value; }
    }

    private void Awake()
    {
        stunned = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello: " + gameObject.name);
        
        HandleLevelStart();
    }

    void FixedUpdate()
    {
        // Generate a plane that intersects the transform's position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.forward, transform.position);

        // Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           
        // Determine the point where the cursor ray intersects the plane.
        // This will be the point that the object must look towards to be looking at the mouse.
        // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
        //   then find the point along that ray that meets that distance.  This will be the point
        //   to look at.
        
        float hitdist = 0.0f;
        
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stunned <= 0)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                OnLevelStart();
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (myRigidBody)
                {
                    Debug.Log("Left");
                    myRigidBody.velocity = new Vector3(-chickSpeed, myRigidBody.velocity.y, myRigidBody.velocity.z);
                    // TODO - add rotation only for mesh
                }
            }

            if (Input.GetKey(KeyCode.D))
            {
                if (myRigidBody)
                {
                    Debug.Log("Right");
                    myRigidBody.velocity = new Vector3(chickSpeed, myRigidBody.velocity.y, myRigidBody.velocity.z);
                    // TODO - add rotation only for mesh
                }
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                HandleHatchEgg();
            }




            if (timeToSurviveLeft <= 0)
            {
                OnSurvive();
            }

            CheckIfTooStressed();
            // Debug.Log("Countdown: " + timeToSurviveInitial + "- stress: " + stress);
        }
        else
        {
            stunned -= Time.deltaTime;
            if (stunned < 0)
            {
                stunned = 0;
            }
        }
    }

    public void BecomeStressed(float stressAmount)
    {
        stress += stressAmount;
    }

    void CheckIfTooStressed()
    {
        
        if(stress >= stressToHatch)
        {
            OnHatchEgg();
        }
    }

    void HandleHatchEgg()
    {
        //        Transform originalPositionOfChicken = gameObject.transform;

        Transform originalEggPoint = eggPoint;


        Debug.Log("egg hatch");
        if(egg) {
            // Push kitchen upwards due to having egg
            myRigidBody.GetComponent<Rigidbody>().AddForce(Vector2.up * eggForce, ForceMode.Impulse);
            // Egg
            Instantiate<GameObject>(egg, eggPoint.position, Quaternion.identity);
        }

        stress = 0;


    }

    void HandleTakeDamage(float damage)
    {
        BecomeStressed(damage * 2.0f);
    }

    private void HandleLevelStart()
    {
        Debug.Log("Start Level");
        timeToSurviveLeft = timeToSurviveInitial;
        Debug.Log("Countdown: " + timeToSurviveInitial + "- stress: " + stress);
        StartCoroutine(StartCountdown());
    }

    private void HandleSurvive()
    {
        Debug.Log("win");
        OnLevelStart();
    }


    void OnEnable()
    {
        OnLevelStart += HandleLevelStart;
        OnSurvive += HandleSurvive;
        OnHatchEgg += HandleHatchEgg;
    }
    void OnDisable()
    {
        OnLevelStart -= HandleLevelStart;
        OnSurvive -= HandleSurvive;
        OnHatchEgg -= HandleHatchEgg;
    }
    public IEnumerator StartCountdown()
    {
        float seconds = 1.0f;
        while (timeToSurviveLeft > 0)
        {

            yield return new WaitForSeconds(1.0f);
            
            timeToSurviveLeft -= seconds;
            BecomeStressed(1.0f);
        }
    }

    
}
