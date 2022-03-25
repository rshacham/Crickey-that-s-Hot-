using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireCreator : MonoBehaviour
{
    [SerializeField] private GameObject fire;
    [SerializeField] private float positionChangeRate;
    private Rigidbody2D myRigid;
    private float posRandomTimer;

    // Start is called before the first frame update
    void Start()
    {
        posRandomTimer = positionChangeRate;
        myRigid = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        print(posRandomTimer);
        if (posRandomTimer <= 0)
        {
            myRigid.position = new Vector2(Random.Range(0, 10), Random.Range(0, 10));
            print(myRigid.position);
            posRandomTimer = positionChangeRate;

        }
        else
        {
            posRandomTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            print("Hit Ground");
            Instantiate(fire, myRigid.position, quaternion.identity);
        }
    }
    
}
