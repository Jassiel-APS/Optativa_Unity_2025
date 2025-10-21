using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Transform bulletPool;
    public List<Bullet> bullets = new();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        for (int i = 0; i < 50; i++)
        {
            var instance = Instantiate(bulletPrefab, bulletPool);
            var bullet = instance.GetComponent<Bullet>();
            bullets.Add(bullet);
            instance.SetActive(false);
        }

        
        while (true)
        {
            var available = bullets.FirstOrDefault(x => !x.gameObject.activeInHierarchy);
            if(available)
            {
                available.transform.position = firePoint.position;
                available.gameObject.SetActive(true);
                available.direction = firePoint.up;
            }
            
            yield return new WaitForSeconds(0.5f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
