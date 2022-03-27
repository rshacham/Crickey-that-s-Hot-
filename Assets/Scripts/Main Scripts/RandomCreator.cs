using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomCreator : MonoBehaviour
{
    [SerializeField] private GameObject whatToCreate;
    [SerializeField] private float positionChangeRate;
    [SerializeField] private Vector2 rangeX;
    [SerializeField] private Vector2 rangeY;
    [SerializeField] private int stopSpawningAtThisAmount;
    [SerializeField] private GameObject world;
    [SerializeField] private LayerMask validLayers;
    
    private Rigidbody2D myRigid;
    private float posRandomTimer;
    private Vector2 basePos = new Vector2(-1.5f, 0.5f);
    private int amountOfItem;
    public int AmountOfItem
    {
        get => amountOfItem;
        set => amountOfItem = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        posRandomTimer = positionChangeRate;
        myRigid = this.gameObject.GetComponent<Rigidbody2D>();
        myRigid.position = basePos + new Vector2(Random.Range((int)rangeX.x, (int)rangeX.y), Random.Range((int)rangeY.x, (int)rangeY.y));
    }

    // Update is called once per frame
    void Update()
    {
        if (amountOfItem<stopSpawningAtThisAmount)
        {
            if (posRandomTimer <= 0)
            {
                Collider2D[] colliders = {};
                while (colliders.Length == 0)
                {
                    myRigid.position = basePos + new Vector2(Random.Range((int)rangeX.x, (int)rangeX.y), Random.Range((int)rangeY.x, (int)rangeY.y));
                    colliders = Physics2D.OverlapCircleAll(myRigid.position, 0.0f, validLayers);
                }
                posRandomTimer = positionChangeRate;
                {
                    Transform newObject = Instantiate(whatToCreate, myRigid.position, quaternion.identity).transform.parent = world.transform;
                    /*
                    newObject.position = new Vector3(newObject.position.x, newObject.position.y, -10);
                    */
                    amountOfItem++;
                }

            }
            else
            {
                posRandomTimer -= Time.deltaTime;
            }   
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

    }
    
}
