using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Fire : MonoBehaviour
{
    [SerializeField] private List<Sprite> fireSprites; //0 is regular, 1 is corner
    [SerializeField] private int fireLife;
    [SerializeField] private float spreadCooldown;
    private bool shouldSpread = true;
    [SerializeField] GameObject fire;

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
        boundaries.x = 40.5f;
        boundaries.y = -13.5f;
        boundaries.z = 7.5f;
        boundaries.w = -10.5f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (CompareTag("WaterBullet"))
        {
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
        int newDirection = 0;
        Vector3 newPosition = new Vector3(0, 0, 0);
        //Remove the oppisate direction, we can't spread to the left after we already spread to the right
        List<int> directions = new List<int> {0, 1, 2, 3};
        if (fireDirection == 0 || fireDirection == 2)
        {
            directions.Remove(fireDirection + 1);
        }
        else
        {
            directions.Remove(fireDirection - 1);
        }
        
        while (directions.Count != 0)
        {
            //print(Random.Range(0, directions.Count - 1));
            int randomInt = Random.Range(0, directions.Count);
            newDirection = directions[randomInt];
            newPosition = NewDirectionPosition(newDirection);
            Collider2D[] fires = Physics2D.OverlapCircleAll(newPosition, 1.4f);
            Collider2D[] koalas = Physics2D.OverlapCircleAll(newPosition, 4f, LayerMask.NameToLayer("Koala"));
            print(fires.Length);
            print(CheckBoundaryDistance(newDirection, 3));
            if (fires.Length == 0 && koalas.Length == 0 && CheckBoundaryDistance(newDirection, 3))
            {
                break;
            }
            directions.Remove(newDirection);
        }

        if (directions.Count == 0)
        {
            return;
        }

        //Vector3 position = transform.position;;
        //print(newDirection);
        GameObject newFire;
        Fire newScript;
        SpriteRenderer newSprite;
        if (ShouldChangeAxis(fireDirection, newDirection))
        {
            SpreadChangeAxis(newDirection, newPosition);
            shouldSpread = false;

            return;
        }
        
        SpreadSameAxis(newDirection, newPosition);
        //     if ((fireDirection == 2))
        // {
        //     if (transform.position.x < boundaries.z - 3)
        //     {
        //         SpreadHorizontal(1);
        //     }
        //
        //     else
        //     {
        //         if (transform.position.x - directionGenerated * 3 > boundaries.w + 3)
        //         {
        //             ChangeDirection(0, -1);
        //         }
        //     }
        // }
        //
        // else if ((fireDirection == 3) && transform.position.x > boundaries.w + 3)
        // {
        //     newFire = Instantiate(fire);
        //     newScript = newFire.GetComponent<Fire>();
        //     newFire.transform.position =
        //         new Vector3(position.x - 3, position.y, position.z);
        //     newSprite = newFire.GetComponent<SpriteRenderer>();
        //     newScript.Boundaries = boundaries;
        //     newSprite.sprite = fireSprites[0];
        // }
        shouldSpread = false;
    }

    private void SpreadHorizontal(int direction)
    // if direction is 1 we spread to the right, if -1 we spread to the left
    {
        Vector3 position = transform.position;
        GameObject newFire;
        Fire newScript;
        SpriteRenderer newSprite;
        newFire = Instantiate(fire);
        newScript = newFire.GetComponent<Fire>();
        newFire.transform.position =
            new Vector3(position.x + 3 * direction, position.y, position.z);
        newFire.transform.rotation = transform.rotation;
        newSprite = newFire.GetComponent<SpriteRenderer>();
        newScript.Boundaries = boundaries;
        newScript.DirectionGenerated = DirectionGenerated + 1;
        newSprite.sprite = fireSprites[0];
    }

    private void ChangeDirection(int axis, int direction)
    // if axis is 0 we're changing in the horizontal axis, if 1 we're changing in the vertical vaxis
    // if direction is 1 we spread to the right/up, if -1 we spread to the left/down
    {
        int newPosition;
        Vector3 position = transform.position;
        GameObject newFire;
        Fire newScript;
        SpriteRenderer newSprite;
        newFire = Instantiate(fire);
        newScript = newFire.GetComponent<Fire>();
        newScript.FireDirection = fireDirection;
        newPosition = direction * 3 * directionGenerated;
        if (axis == 0)
        {
            newFire.transform.position =
                new Vector3(position.x + newPosition, position.y, position.z);
        }
        else
        {
            newFire.transform.position = new Vector3(position.x, position.y + newPosition, position.z);
        }

        newFire.transform.rotation = transform.rotation;
        newSprite = newFire.GetComponent<SpriteRenderer>();
        newScript.Boundaries = boundaries;
        newScript.DirectionGenerated = DirectionGenerated + 1;
        newSprite.sprite = fireSprites[0];
    }

    private void SpreadSameAxis(int newDirection, Vector3 newPosition)
    {
        GameObject newFire;
        newFire = Instantiate(fire);
        Fire newScript = newFire.GetComponent<Fire>();
        SpriteRenderer newSprite = newFire.GetComponent<SpriteRenderer>();
        newSprite.sprite = fireSprites[0];
        newFire.transform.position = newPosition;
        newScript.Boundaries = boundaries;
        newScript.spreadCooldown = spreadCooldown;
        newScript.FireDirection = newDirection;
        newFire.transform.rotation = transform.rotation;

    }
    private void SpreadChangeAxis(int newDirection, Vector3 newPosition)
    {
        GameObject newFire;
        newFire = Instantiate(fire);
        Quaternion rotation = transform.rotation;
        Fire newScript = newFire.GetComponent<Fire>();
        SpriteRenderer newSprite = newFire.GetComponent<SpriteRenderer>();
        newSprite.sprite = fireSprites[1];
        newFire.transform.position = newPosition;
        newScript.Boundaries = boundaries;
        newScript.spreadCooldown = spreadCooldown;
        newScript.FireDirection = newDirection;
        newFire.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, Mathf.Deg2Rad* NewRotation(fireDirection, newDirection));
    }

    private Vector3 NewDirectionPosition(int direction)
    {
        Vector3 curPosition = transform.position;
        switch (direction)
        {
            case 0:
                return new Vector3(curPosition.x, curPosition.y + 3, curPosition.z);
            case 1:
                return new Vector3(curPosition.x, curPosition.y - 3, curPosition.z);
            case 2:
                return new Vector3(curPosition.x + 3, curPosition.y, curPosition.z);
            case 3:
                return new Vector3(curPosition.x - 3, curPosition.y, curPosition.z);
        }
        
        return new Vector3(0, 0, 0); // Not sure how can i tell the function that if direction
                                     // is not in the range of 0-3, don't return anything
    }

    private bool CheckBoundaryDistance(int direction, float distance)
    {
        Vector3 curPosition = transform.position;
        print(transform.position);
        print(direction);
        switch (direction)
        {
            case 0:
                return ((curPosition.y + distance) < boundaries.x);
            case 1:
                return ((curPosition.y - distance) > boundaries.y);
            case 2:
                return ((curPosition.x + distance) < boundaries.z);
            case 3:
                return ((curPosition.x - distance) > boundaries.w);
        }

        return false;
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

}
