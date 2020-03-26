using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{


    NavMeshAgent nav;
    public GameObject ExplodePartical;
    GameObject player;
    public bool idle;
    public float distance;
    public float minDistanceToAttack = 4f;
    public int Damage = 25;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = gameObject.GetComponent<Animator>();
        if(anim != null)
        anim.SetFloat("Forward", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        Move();
    }



    public void HitAtTop()
    {
        Invoke("DestroyMonster", 0.1f);
    }

    public void DestroyMonster()
    {
        Instantiate(ExplodePartical, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void Move()
    {

        if (idle)
            
            
            return;


          else if (idle == false)
            {
    
            distance =  Vector3.Distance(transform.position, player.transform.position);

            if (distance <= minDistanceToAttack)
            {
                if (anim != null)
                {
                    anim.SetFloat("Forward", 1);
                    nav.SetDestination(player.transform.position);
                }
            }

            else
            {
                anim.SetFloat("Forward", 0);
                nav.SetDestination(transform.position);
            }

            }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            // knock back player 

            Vector3 hitDirection = other.transform.position - transform.position;
            hitDirection = hitDirection.normalized;

            other.gameObject.GetComponent<Movement>().KnockBack(hitDirection);
            // do damage to player
            other.gameObject.GetComponent<PlayerInfo>().TakeDamage(Damage);
            
        }
    }

}
