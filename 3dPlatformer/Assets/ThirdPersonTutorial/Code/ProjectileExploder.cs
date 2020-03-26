using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExploder : MonoBehaviour
{

    public float Speed = 3f;
    public GameObject ExplodePartical;
    public float DestroyTime = 3.5f;

    public int Damage = 5;
    public void Start()
    {
        StartCoroutine(destroyPrejectile());
    }
    public void Update()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.forward * -Speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(ExplodePartical != null)
            Instantiate(ExplodePartical, transform.position, Quaternion.identity);


            Vector3 hitDirection = other.transform.position - transform.position;
            hitDirection = hitDirection.normalized;

            other.gameObject.GetComponent<Movement>().KnockBack(hitDirection);
            // do damage to player
            other.gameObject.GetComponent<PlayerInfo>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }

    IEnumerator destroyPrejectile()
    {

        yield return new WaitForSeconds(DestroyTime);

        if (ExplodePartical != null)
            Instantiate(ExplodePartical, transform.position, Quaternion.identity);


        if (gameObject != null)
            Destroy(gameObject);
    }
}
