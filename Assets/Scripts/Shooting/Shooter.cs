using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject waterBullet;
    [SerializeField] private List<GameObject> waterBullets;
    [SerializeField] private float shotPower; //Power of the egg shot

    private int bulletsAmount = 15;
    // Start is called before the first frame update
    void Start()
    {
        GameObject temp;
        for (int i = 0; i < bulletsAmount; i++)
        {
            temp = Instantiate(waterBullet);
            temp.SetActive(false);
            waterBullets.Add(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Shoot(Vector2 direction)
    {
        GameObject temp;
        Rigidbody2D rigid;
        for (int i = 0; i < bulletsAmount; i++)
        {
            if (!waterBullets[i].activeInHierarchy)
            {
                temp = waterBullets[i];
                temp.transform.position = transform.position;
                temp.transform.rotation = transform.rotation;
                temp.SetActive(true);
                WaterBullet bulletScript = temp.GetComponent<WaterBullet>();
                bulletScript.Direction = direction.normalized;
                bulletScript.ShotPower = shotPower;
                //rigid = temp.GetComponent<Rigidbody2D>();
                //rigid.velocity = direction * shotPower;
                //SoundManager._shared.PlaySound("shotSound");
                break;
            }
        }
    }
}
