using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Runner myRunner;

    public GameObject MiniSprite
    {
        get => miniSprite;
        set => miniSprite = value;
    }
    private GameObject miniSprite;
    // Start is called before the first frame update
    void Start()
    {
        myRunner = FindObjectOfType<Runner>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = myRunner.transform.rotation;
    }
}
