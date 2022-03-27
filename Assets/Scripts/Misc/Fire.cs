using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class Fire : MonoBehaviour
{
    [SerializeField] private List<Sprite> fireSprites; //0 is regular, 1 is corner
    [SerializeField] private int fireLife;
    [SerializeField] private float spreadCooldown;
    private bool shouldSpread = true;
    [SerializeField] GameObject fire;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask koalaLayer;
    [SerializeField] private float spreadDistance; //Distance between x/y position of 2 adjacent fires(basically the size of the sprite)
    [SerializeField] private float spreadFix; //Fix for the x/y position(relevant only when we change axis twice in a row)
    [SerializeField] private bool rotateFire;
    private Runner myRunner;
    
    public int PreviousDirection
    {
        get => previousDirection;
        set => previousDirection = value;
    }
    private int previousDirection;

    public bool AfterAxisChange
    {
        get => afterAxisChange;
        set => afterAxisChange = value;
    }
    private bool afterAxisChange;

    public int FireDirection
    {
        get => fireDirection;
        set => fireDirection = value;
    }
    private int fireDirection = 2; // 0 is up, 1 is down, 2 is right, 3 is left

    public int DirectionGenerated
    {
        get => directionGenerated;
        set => directionGenerated = value;
    }
    private int directionGenerated = 0; // How many fires were generated in current direction

    public Vector4 Boundaries
    {
        get => boundaries;
        set => boundaries = value;
    }
    private Vector4 boundaries; // vector will hold:
    // (upper limit, lower limit, right limit, left limit)
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(DelaySpread());
        GameManager._shared.CurFires++;
        myRunner = FindObjectOfType<Runner>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WaterBullet"))
        {
            fireLife -= 1;
            other.gameObject.SetActive(false);
        }
        
        if (other.CompareTag("Player"))
        {
            GameManager._shared.MyLife -= 1; ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fireLife == 0)
        {
            Destroy(gameObject);
            GameManager._shared.CurFires--;
            
        }

        if (rotateFire)
        {
            transform.rotation = myRunner.transform.rotation;

        }

    }
    
    IEnumerator DelaySpread()
    {
        while (shouldSpread)
        {
            yield return new WaitForSeconds(spreadCooldown);
            Spread();
        }
    }

    private void Spread()
    {
        Vector3 newPosition;
        previousDirection = fireDirection;
        int newDirection = fireDirection;

        newPosition = NewDirectionPosition(newDirection, spreadDistance);
        Collider2D[] grounds = Physics2D.OverlapCircleAll(newPosition, 0f, groundLayer);

        if (grounds.Length != 0)
        {
            newPosition = NewDirectionPosition(newDirection, spreadDistance);
            Collider2D[] koalas = Physics2D.OverlapCircleAll(newPosition, 0.5f, koalaLayer);
            if (koalas.Length != 0)
            {
                shouldSpread = false;
                return;
            }
            SpreadSameAxis(newDirection, newPosition);
            shouldSpread = false;
            return;
        }



        Vector3 curPosition = transform.position;
        newPosition = new Vector3(curPosition.x, curPosition.y + spreadDistance, curPosition.z);
        if (newDirection == 2)
        {
            newDirection = 3;
        }

        else
        {
            newDirection = 2;
        }
        Collider2D[] moalas = Physics2D.OverlapCircleAll(newPosition, 0.5f, koalaLayer);
        if (moalas.Length != 0)
        {
            shouldSpread = false;
            return;
        }
        SpreadSameAxis(newDirection, newPosition);
        shouldSpread = false;
    }

    private void SpreadSameAxis(int newDirection, Vector3 newPosition)
    {
        GameObject newFire;
        newFire = Instantiate(fire, newPosition, quaternion.identity);
        Quaternion rotation = quaternion.identity;
        // newFire.transform.rotation = rotation;
        Fire newScript = newFire.GetComponent<Fire>();
        SpriteRenderer newSprite = newFire.GetComponent<SpriteRenderer>();
        newSprite.sprite = fireSprites[0];
        newScript.Boundaries = boundaries;
        newScript.spreadCooldown = spreadCooldown;
        newScript.FireDirection = newDirection;

    }


    

    private Vector3 NewDirectionPosition(int newDirection, float distance)
    {
        Vector3 curPosition = transform.position;
        switch (fireDirection)
        {
            case 0:
                return new Vector3(curPosition.x, curPosition.y + distance, curPosition.z);
            case 1:
                return new Vector3(curPosition.x, curPosition.y - distance, curPosition.z);
            case 2:
                return new Vector3(curPosition.x + distance, curPosition.y, curPosition.z);
            case 3:
                return new Vector3(curPosition.x - distance, curPosition.y, curPosition.z);
        }
        
        return new Vector3(0, 0, 0); // Not sure how can i tell the function that if direction
                                     // is not in the range of 0-3, don't return anything
    }

    private float NewRotation(int oldDirection, int newDirection)
    {
        switch (oldDirection)
        {
            case 0:
                switch (newDirection)
                {
                    case 3:
                        return 0;
                    case 2:
                        return 90;
                }
                
                break;
            case 1:
                switch (newDirection)
                {
                    case 2:
                        return 180;
                    case 3:
                        return 270;
                }
    
                break;
            case 2:
                switch (newDirection)
                {
                    case 1:
                        return 0;
                    case 0:
                        return 270;
                }
    
                break;
            case 3:
                switch (newDirection)
                {
                    case 1:
                        return 90;
                    case 0:
                        return 180;
                }
    
                break;
        }
    
        return 0;
    }

    private bool ShouldChangeAxis(int oldDirection, int newDirection)
    {
        if (oldDirection == 0 || oldDirection == 1)
        {
            if (newDirection == 2 || newDirection == 3)
            {
                return true;
            }

            return false;
        }

        
        if (oldDirection == 2 || oldDirection == 3)
        {
            if (newDirection == 0 || newDirection == 1)
            {
                return true;
            }
        }

        return false;
    }

    private Vector3 FixPosition(int newDirection, Vector3 newPosition)
    {
        switch (newDirection)
        {
            case 0:
                switch (fireDirection)
                {
                    case 2:
                        return new Vector3(newPosition.x, newPosition.y - spreadFix, newPosition.z);
                    case 3:
                        return new Vector3(newPosition.x, newPosition.y - spreadFix, newPosition.z);

                }

                break;

            case 1:
                switch (fireDirection)
                {
                    // case 2:
                    //     return new Vector3(newPosition.x, newPosition.y + spreadFix, newPosition.z);
                    case 3:
                        return new Vector3(newPosition.x, newPosition.y + spreadFix, newPosition.z);

                }

                break;

            case 2:
                switch (fireDirection)
                {
                    case 0:
                        return new Vector3(newPosition.x - spreadFix, newPosition.y, newPosition.z);
                    case 1:
                        return new Vector3(newPosition.x - spreadFix, newPosition.y, newPosition.z);

                }

                break;

        }

        return newPosition;
    }
    
    private Vector3 FixPositionAfterChange(Vector3 newPosition)
    {
        print(fireDirection);
        print(previousDirection);
        if (previousDirection == 2 && fireDirection == 1 || previousDirection == 3 && fireDirection == 1)
        {
            return new Vector3(newPosition.x, newPosition.y - spreadDistance, newPosition.z);
        }

        if (previousDirection == 2 && fireDirection == 0 || previousDirection == 3 && fireDirection == 0)
        {
            return new Vector3(newPosition.x, newPosition.y + spreadDistance, newPosition.z);
        }

        return (new Vector3(0, 0, 0));
    }

    IEnumerator ChangeDelayes()
    {
        yield return new WaitForSeconds(spreadCooldown);
    }

}
