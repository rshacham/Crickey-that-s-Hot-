using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private int fireLife;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CompareTag("WaterBullet"))
        {
            print("hey");
            fireLife -= 1;
            other.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (fireLife == 0)
        {
            Destroy(gameObject);
        }
    }
}
