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
                myRigid.position = basePos + new Vector2(Random.Range((int)rangeX.x, (int)rangeX.y), Random.Range((int)rangeY.x, (int)rangeY.y));
                posRandomTimer = positionChangeRate;

            }
            else
            {
                posRandomTimer -= Time.deltaTime;
            }   
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") & !GameManager._shared.Rotating())
        {
            Instantiate(whatToCreate, myRigid.position, quaternion.identity).transform.parent = world.transform;
            amountOfItem++;
        }
    }
    
}
