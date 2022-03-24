using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager _shared;
        
    
    public bool TurningRight
    {
        get => turningRight;
        set => turningRight = value;
    }
    private bool turningRight; //If true the player last move was to the right, else it was false
    // Start is called before the first frame update
    void Start()
    {
        _shared = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
