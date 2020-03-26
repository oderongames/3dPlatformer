using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Coin,
        PowerUp,
        Key,
        WorldKey,
        Health,
    }
    public enum RotDirection
    {
        Right,
        forward,
        up,

    }

    public string ItemAnimName = "Collected";
    public int Amount = 1;
    public RotDirection direction;
    public ItemType type;
    public float SpinSpeed = 5f;
    public GameObject ParticalEffect;
    public AudioSource pickupSound;
    Animator anim;

    // PlayerStats stats;
    // Use this for initialization
    void Start()
    {
        //  stats = FindObjectOfType<PlayerStats>();
        anim = GetComponent<Animator>();
        anim.SetBool(ItemAnimName, false);
    }

    // Update is called once per frame
    void Update()
    {

        if (direction == RotDirection.forward)
        {
            transform.Rotate(Vector3.forward * SpinSpeed * Time.deltaTime);
        }
        if (direction == RotDirection.Right)
        {
            transform.Rotate(Vector3.right * SpinSpeed * Time.deltaTime);
        }
        if (direction == RotDirection.up)
        {
            transform.Rotate(Vector3.up * SpinSpeed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            if(pickupSound != null)
            pickupSound.Play();

            if(ParticalEffect != null)
            Instantiate(ParticalEffect, transform.position, transform.rotation);

            if (type == ItemType.Coin)
            {
                // add a coin
                //  stats.AddCoins(1);
                //anim.SetBool(ItemAnimName, true);

                other.gameObject.GetComponent<PlayerInfo>().PickUpCoint();
            }
            if (type == ItemType.PowerUp)
            {

            }
            if (type == ItemType.Key)
            {

            }
            if (type == ItemType.WorldKey)
            {

            }
            if (type == ItemType.Health)
            {

                // stats.GainHealth((float)Amount);
            }
            Destroy(gameObject, 0.02f);
        } 
    }
}
