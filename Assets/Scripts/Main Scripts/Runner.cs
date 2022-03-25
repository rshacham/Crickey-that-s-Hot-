using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Runner : MonoBehaviour
{
    [SerializeField] private float runnerSpeed; // How fast will the runner move
    [SerializeField] private int rotationSpeed; //How fast will the camera rotate
    [SerializeField] private GameObject myWorld;
    [SerializeField] private Camera myCamera;
    [SerializeField] private Transform runnerTransform;

    //private Transform runnerTransform;

    private Rigidbody2D myRigid;
    private Vector2 myMovement;
    private Vector2 mousePosition;
    private int rotate = 0;
    private Shooter myShooter;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        myShooter = GetComponentInChildren<Shooter>();
    }

    // Update is called once per frame
    void Update()
    {

        mousePosition = myCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 shooterPosition = myShooter.transform.position;
        Vector2 shootingDirection = mousePosition - new Vector2(shooterPosition.x, shooterPosition.y); //Calculates a vector to where the runner is currently "looking"
        print(shootingDirection);
        // print(mousePosition);
        // print(myShooter.transform.position);
        if (rotate == 0)
        {
            myMovement.y = 1; // Always move forward
            myMovement.x = Input.GetAxisRaw("Horizontal"); 
            if (Input.GetKeyDown(KeyCode.A))
            {
                GameManager._shared.TurningRight = false;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                GameManager._shared.TurningRight = true;
            }
        }

        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     // world.transform.Rotate(new Vector3(0,0,1),-90);
        //     rotate += 18 * rotationSpeed;
        // }
        //
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     rotate -= 18 * rotationSpeed;
        // }

        if (Input.GetMouseButtonDown(0))
        {
            //Vector3 shooterPosition = myShooter.transform.position;
            //Vector2 shootingDirection = mousePosition - new Vector2(shooterPosition.x, shooterPosition.y); //Calculates a vector to where the runner is currently "looking"
            //float rotationRad = (transform.localEulerAngles.z + 90) * Mathf.Deg2Rad;
            //shootingDirection = new Vector2(Mathf.Cos(rotationRad), Mathf.Sin(rotationRad));
            if (shootingDirection.y < 0.4)
            {
                shootingDirection.y = 0.4f;
            }
            myShooter.Shoot(shootingDirection.normalized);
        }


    }

    private void FixedUpdate()
    {
        Vector2 lookingDirection = mousePosition - myRigid.position; //Calculates a vector to where the runner is currently "looking"
        // angle = Mathf.Atan2(lookingDirection.y, lookingDirection.x) * Mathf.Rad2Deg - 90; 
        // if (angle >= 0 && angle < 90)
        // {
        //     myRigid.rotation = 0;
        // }
        //
        // else if (angle < -180)
        // {
        //     myRigid.rotation = -180;
        // }
        //
        // else
        // {
        //     myRigid.rotation = angle;
        // }
        if (rotate == 0) // Controls the movement of the runner, we only want this to occur if rotation is 0
        {
            myRigid.MovePosition(myRigid.position + myMovement * runnerSpeed * Time.fixedDeltaTime);
        }
        
        if (rotate > 0)
        {
            // transform.Rotate(new Vector3(0, 0, 1), (-5f / rotationSpeed));
            myWorld.transform.Rotate(new Vector3(0, 0, -1), (-5f / rotationSpeed));
            rotate--;
        }

        if (rotate < 0)
        {
            // transform.Rotate(new Vector3(0, 0, 1), (-5f / rotationSpeed));
            myWorld.transform.Rotate(new Vector3(0, 0, 1), (-5f / rotationSpeed));
            rotate++;
        }
    }

    public void Turn()
    {
        if (GameManager._shared.TurningRight)
        {
            rotate += 18 * rotationSpeed;
        }

        else
        {
            rotate -= 18 * rotationSpeed;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            myRigid.position = Vector2.zero;
        }
    }
}
