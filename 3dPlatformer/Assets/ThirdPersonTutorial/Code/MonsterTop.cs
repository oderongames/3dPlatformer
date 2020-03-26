using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTop : MonoBehaviour
{
    Monster monster;

    public void Start()
    {
        monster = GetComponentInParent<Monster>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if( other.gameObject.tag == "Player")
        {
            monster.HitAtTop();
            other.gameObject.GetComponent<Movement>().InAirJump();
        }
    }
}
