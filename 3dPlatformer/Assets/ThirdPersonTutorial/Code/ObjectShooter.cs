using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShooter : MonoBehaviour
{

    public GameObject Projectile;

    public GameObject ParticalExplosion;
    public Transform SpawnPoint;

    public float ShootRate = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(repeatShooting());
    }


    IEnumerator repeatShooting() {

        yield return new WaitForSeconds(ShootRate);
        ShootProjectile();
        StartCoroutine(repeatShooting());

     }
    public void ShootProjectile()
    {
        if (ParticalExplosion != null) 
        Instantiate(ParticalExplosion, SpawnPoint.transform.position, SpawnPoint.transform.rotation);

        if (Projectile != null)
        {
            GameObject Go = Instantiate(Projectile, SpawnPoint.transform.position, Quaternion.identity);

         
            
        }
    }
}
