using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Runner : MonoBehaviour
{
    enum direction
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }
    [SerializeField] private float runnerSpeed; // How fast will the runner move
    [SerializeField] private float runnerSideSpeed; // How fast will the runner move side to side. higher = slower
    [SerializeField] private float runnerSideBraking;// Higher = stronger braking
    [SerializeField] private int rotationSpeed; //How fast will the camera rotate
    [SerializeField] private GameObject myWorld;
    [SerializeField] private Camera myCamera;
    [SerializeField] private Transform runnerTransform;
    [SerializeField] private float shootingFix; //We'll correct shootingDirection coordinate to this number, to prevent the runner from shooting backwards
    

    //private Transform runnerTransform;

    private Rigidbody2D myRigid;
    private Vector2 myMovement;
    private Vector2 mousePosition;
    private int rotate = 0;
    private Shooter myShooter;
    private float angle;
    private bool isMoving;
    private int dirNum;
    private float speedVarFromOutside = 0;
    private Vector2 sidewaysVector; // vector 2 that is vertical to the runner's object
    public float OutsideVarSpeed
    {
        get => speedVarFromOutside;
        set => speedVarFromOutside = value;
    }
    public int Rotate
    {
        get => rotate;
    }
    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        myShooter = GetComponentInChildren<Shooter>();
    }

    // Update is called once per frame
    void Update()
    {
        float eulerRotation = transform.rotation.eulerAngles.z;

        mousePosition = myCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 shooterPosition = myShooter.transform.position;
        if (Math.Abs(eulerRotation - 90f) < 0.1f || Math.Abs(eulerRotation - 270f) < 0.1f)
        {
            sidewaysVector = (new Vector2(shooterPosition.x - 1, shooterPosition.y) - (Vector2)shooterPosition).normalized;
        }

        else
        {
            {
                sidewaysVector = (new Vector2(shooterPosition.x, shooterPosition.y - 1) - (Vector2)shooterPosition).normalized;

            }
        }
        Vector2 shootingDirection = mousePosition - new Vector2(shooterPosition.x, shooterPosition.y); //Calculates a vector to where the runner is currently "looking"
        if (rotate == 0)
        {
            myMovement.y = 1; // Always move forward
            myMovement.x = -Input.GetAxisRaw("Horizontal");
        }
        Vector2 brakeVector = myRigid.velocity;
        if (dirNum % 2 == 0)
        {
            brakeVector.x *= 1-runnerSideBraking/100;
        }
        else
        {
            brakeVector.y *= 1-runnerSideBraking/100;
        }
            
        myRigid.velocity = brakeVector;
        if (Input.GetKey(KeyCode.D))
        {
            myRigid.AddForce(transform.right/runnerSideSpeed,ForceMode2D.Impulse);
        }else if (Input.GetKey(KeyCode.A))
        {
            myRigid.AddForce(-transform.right/runnerSideSpeed,ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // world.transform.Rotate(new Vector3(0,0,1),-90);
            rotate += 18 * rotationSpeed;
            dirNum--;
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            rotate -= 18 * rotationSpeed;
            dirNum++;
        }

        if (Input.GetMouseButtonDown(0) && !GameManager._shared.Rotating())
        {
            shootingDirection = ShootingDirectionFix(eulerRotation, shootingDirection);
            myShooter.Shoot(shootingDirection.normalized);
        }
        dirNum = (4 + dirNum) % 4;
    }

    private Vector2 ShootingDirectionFix(float eulerRotation, Vector2 shootingDirection)
    {
        if (Math.Abs(eulerRotation) < 0.1f && shootingDirection.y < shootingFix)
        {
            shootingDirection.y = shootingFix;
        }

        if (Math.Abs(eulerRotation - 90) < 0.1f && shootingDirection.x > shootingFix)
        {
            shootingDirection.x = shootingFix;
        }

        if (Math.Abs(eulerRotation - 180) < 0.1f && shootingDirection.y > shootingFix)
        {
            shootingDirection.y = shootingFix;
        }

        if (Math.Abs(eulerRotation - 270) < 0.1f && shootingDirection.x < shootingFix)
        {
            shootingDirection.x = shootingFix;
        }

        return shootingDirection;
    }

    private void FixedUpdate()
    { 
        float currSpeed = runnerSpeed + OutsideVarSpeed;
        Vector2 lookingDirection = mousePosition - myRigid.position; //Calculates a vector to where the runner is currently "looking"
        if (rotate == 0 & !isMoving) // Controls the movement of the runner, we only want this to occur if rotation is 0
        {
            // myRigid.MovePosition(myRigid.position + myMovement * runnerSpeed * Time.fixedDeltaTime);
            myRigid.AddForce(transform.up*currSpeed,ForceMode2D.Impulse);
            isMoving = true;
        }
        
        if (rotate > 0)
        {
            transform.Rotate(new Vector3(0, 0, -1), (-5f / rotationSpeed));
            // myWorld.transform.Rotate(new Vector3(0, 0, -1), (-5f / rotationSpeed));
            myRigid.velocity = Vector2.zero;
            rotate--;
            isMoving = false;
        }

        if (rotate < 0)
        {
            transform.Rotate(new Vector3(0, 0, 1), (-5f / rotationSpeed));
            // myWorld.transform.Rotate(new Vector3(0, 0, 1), (-5f / rotationSpeed));
            myRigid.velocity = Vector2.zero;
            rotate++;
            isMoving = false;

        }
    }

    public void Turn(bool turnDir)
    {
        if (turnDir)
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
        if (other.CompareTag("Koala"))
        {
            GameManager._shared.GotKoala();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Fire")) //TODO hitting a fire mechanic, probably game over
        {
        }

        if (other.CompareTag("Fog"))
        {
            other.gameObject.SetActive(false);
        }
        // if (other.CompareTag("Turn"))
        // {
        //     print("turn");
        //     Turn(false);
        // }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall") & (dirNum == 0 | dirNum == 2))
        {
            SceneManager.LoadScene("Beta");
            
        }
        
        if (other.gameObject.CompareTag("Wall side") & (dirNum == 1 | dirNum == 3))
        {
            SceneManager.LoadScene("Beta");
        }
    }
}
