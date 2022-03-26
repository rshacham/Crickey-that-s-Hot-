using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private RandomCreator koalaSpawner;
    [SerializeField] private RandomCreator fireSpawner;
    [SerializeField] private Runner runner;
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private float koalaEffectOnSpeed;

    
    private int score;
    void Start()
    {
        _shared = this;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotKoala()
    {
        koalaSpawner.AmountOfItem -= 1;
        score++;
        scoreUI.text = score.ToString();
        runner.OutsideVarSpeed += koalaEffectOnSpeed;
    }

    public bool Rotating()
    {
        return runner.Rotate != 0;
    }
}
