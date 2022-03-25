using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turns : MonoBehaviour
{
    private bool coolDown;
    [SerializeField] private bool leftTurn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("left");
        Runner temp = other.gameObject.GetComponent<Runner>();
        temp.Turn(leftTurn);
    }

    private IEnumerator CoolDown()
    {
        while (coolDown)
        {
            yield return new WaitForSeconds(5);
            
        }
    }
}
