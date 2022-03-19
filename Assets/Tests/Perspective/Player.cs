using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rigid;
    public GameObject world;

    public float speed;
    public int rotationSpeed;

    private int rotate = 0;
    // Start is called before the first frame update
    void Start()
    {
        rotate = 0;

        // rigid.velocity = Vector2.right*speed;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        if (rotate == 0)
        {
            Vector2 loc = rigid.position;
            loc.x += Time.deltaTime * speed;
            rigid.position = loc;
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            // world.transform.Rotate(new Vector3(0,0,1),-90);
            rotate += 18*rotationSpeed;
        }

        if (rotate >0)
        {
            world.transform.Rotate(new Vector3(0,0,1),(-5f / rotationSpeed));
            rotate--;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("hey");
    }
}
