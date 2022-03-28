using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRunner;
    [SerializeField] private bool followRotation;
    [SerializeField] private bool followPosition;
    [SerializeField] private int height1;
    [SerializeField] private int height2;
    [SerializeField] private LayerMask upperCam;
    [SerializeField] private LayerMask lowerCam;

    private bool up;
    // Start is called before the first frame update
    void Start()
    {
        up = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (followPosition)
        {
            transform.position = new Vector3(myRunner.position.x, myRunner.position.y, transform.position.z);
        }

        if (followRotation)
        {
            transform.rotation = myRunner.transform.rotation;
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            Vector3 pos = transform.position;
            pos.z = up ? -height1 : -height2;
            up = !up;
            transform.position = pos;
        }
    }
}
