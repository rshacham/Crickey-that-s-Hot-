using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{

    [SerializeField] private float runnerSpeed;
    [SerializeField] private int rotationSpeed;
    [SerializeField] private GameObject myWorld;

    private Rigidbody2D myRigid;

    private int rotate = 0;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (rotate == 0)
        {
            Vector2 loc = myRigid.position;
            loc.x += Time.deltaTime * runnerSpeed;
            myRigid.position = loc;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            // world.transform.Rotate(new Vector3(0,0,1),-90);
            rotate += 18 * rotationSpeed;
        }

        if (rotate > 0)
        {
            myWorld.transform.Rotate(new Vector3(0, 0, 1), (-5f / rotationSpeed));
            rotate--;
        }
    }
}
