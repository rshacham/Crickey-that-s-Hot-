using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// This class is made to generate a game object on the tile map
public class ObjectGenerator : MonoBehaviour
{
    [SerializeField] private GameObject myObject;
    [SerializeField] private Vector4 squareBounderies; // vector will hold:
    // (upper left limit, upper right limit, lower left limit, lower right limit)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
