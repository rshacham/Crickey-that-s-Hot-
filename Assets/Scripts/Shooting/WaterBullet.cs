using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class WaterBullet : MonoBehaviour
{
    public Vector2 Direction
    {
        get => direction;
        set => direction = value;
    }
    private Vector2 direction;

    public float ShotPower
    {
        get => shotPower;
        set => shotPower = value;
    }
    private float shotPower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(SelfDestroy());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator SelfDestroy()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + direction.x * shotPower * Time.fixedDeltaTime,
            transform.position.y + direction.y * shotPower * Time.fixedDeltaTime, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // print(other.gameObject.layer);
        // if (other.gameObject.layer == 12) // Layer is "Walls"
        // {
        //     gameObject.SetActive(false);
        // }
    }
}
