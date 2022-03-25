using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Runner : MonoBehaviour
{

    [SerializeField] private float runnerSpeed;
    [SerializeField] private int rotationSpeed;
    [SerializeField] private GameObject myWorld;
    [SerializeField] private Camera myCamera;
    [SerializeField] private Transform runnerTransform;

    //private Transform runnerTransform;
    private Vector2 lookingDirection;
    private Vector2 shootingDirection;
    private Rigidbody2D myRigid;
    private Vector2 myMovement;
    private Vector2 mousePosition;
    private int rotate = 0;
    private Shooter myShooter;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        //runnerTransform = GetComponentInParent<Transform>();
        myRigid = GetComponent<Rigidbody2D>();
        myShooter = GetComponentInChildren<Shooter>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = myCamera.ScreenToWorldPoint(Input.mousePosition);
        if (rotate == 0)
        {
            myMovement.x = 1;
            myMovement.y = Input.GetAxisRaw("Vertical");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            // world.transform.Rotate(new Vector3(0,0,1),-90);
            rotate += 18 * rotationSpeed;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            rotate -= 18 * rotationSpeed;
        }

        if (Input.GetMouseButtonDown(0))
        {
            float rotationRad = (transform.localEulerAngles.z + 90) * Mathf.Deg2Rad;
            shootingDirection = new Vector2(Mathf.Cos(rotationRad), Mathf.Sin(rotationRad));
            myShooter.Shoot(shootingDirection.normalized);
        }


    }

    private void FixedUpdate()
    {

        lookingDirection = mousePosition - myRigid.position; //Calculates a vector to where the runner is currently "looking"
        angle = Mathf.Atan2(lookingDirection.y, lookingDirection.x) * Mathf.Rad2Deg - 90;
        print(angle);
        if (angle >= 0 && angle < 90)
        {
            myRigid.rotation = 0;
        }
        
        else if (angle < -180)
        {
            myRigid.rotation = -180;
        }
        
        else
        {
            myRigid.rotation = angle;
        }
        if (rotate == 0)
        {
            myRigid.MovePosition(myRigid.position + myMovement * runnerSpeed * Time.fixedDeltaTime);

        }
        
        if (rotate > 0)
        {
            // transform.Rotate(new Vector3(0, 0, 1), (-5f / rotationSpeed));
            myWorld.transform.Rotate(new Vector3(0, 0, 1), (-5f / rotationSpeed));
            rotate--;
        }

        if (rotate < 0)
        {
            // transform.Rotate(new Vector3(0, 0, 1), (-5f / rotationSpeed));
            myWorld.transform.Rotate(new Vector3(0, 0, -1), (-5f / rotationSpeed));
            rotate++;
        }
    }
}
