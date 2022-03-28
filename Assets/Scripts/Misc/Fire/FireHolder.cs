using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHolder : MonoBehaviour
{
    private Transform myTransform;

    public List<Transform> myFires;
    private Vector3 startingScale;

    [SerializeField] private float moderateGrowth; //The bigger this is, the growth will be more moderate
    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        startingScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (myFires.Count > 0)
        {
            // FixPosition();
        }

        myTransform.localScale = startingScale * myFires.Count / moderateGrowth;
    }

    public void FixPosition()
    {
        float avgX = 0, avgY = 0;
        foreach (var fire in myFires)
        {
            avgX += fire.position.x;
            avgY += fire.position.y;
        }

        avgX = avgX / myFires.Count;
        avgY = avgY / myFires.Count;
        transform.position = new Vector3(avgX, avgY, transform.position.z);
    }
}
