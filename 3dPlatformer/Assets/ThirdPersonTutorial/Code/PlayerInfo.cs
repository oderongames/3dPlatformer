using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{

    public Slider HealthBar;
    public Text CointText;
    public Transform SpawnPoint;
    public GameObject DiePartical;
    public int MaxHealth = 100;
    public int CurrentHealth = 100;
    public float respawnTime = 3f;
    public int Coins;

    Movement movement;


    // invencibility
    public float invincibilityLength = 1f;
    private float InvinciblityCounter;

    // flash
    public Renderer playerRenderer;
    private float flashCounter;
    public float flashLenght = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        playerRenderer = gameObject.GetComponentInChildren<Renderer>();
        movement = GetComponent<Movement>();
        StartPlayer();
      
        
    }

    // Update is called once per frame
    void Update()
    {
        if(InvinciblityCounter > 0)
        {
            InvinciblityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashLenght;
            }

            if(InvinciblityCounter <= 0)
            {
                playerRenderer.enabled = true;
            }
        }
    }


  
    public void TakeDamage(int Damage)
    {
        if (InvinciblityCounter <= 0)
        {

            CurrentHealth -= Damage;
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                // player has died respawn
                PlayerDied();
            }

            HealthBar.value = CurrentHealth;

            InvinciblityCounter = invincibilityLength;

            playerRenderer.enabled = false;
            flashCounter = flashLenght;
        }
    }

    public IEnumerator ResetPlayer()
    {
        yield return new WaitForSeconds(respawnTime);
        gameObject.GetComponentInChildren<Renderer>().enabled = true;
        transform.position = SpawnPoint.position;
        CurrentHealth = MaxHealth;
        movement.canMove = true;
        
        HealthBar.value = CurrentHealth;

    }



    public void StartPlayer()
    {

        HealthBar.maxValue = MaxHealth;
        HealthBar.value = CurrentHealth;
        gameObject.GetComponentInChildren<Renderer>().enabled = true;
        transform.position = SpawnPoint.position;
        CurrentHealth = MaxHealth;
        movement.canMove = true;

        HealthBar.value = CurrentHealth;
        CointText.text = "COINS: 0";

    }

    public void PlayerDied()
    {
        if (DiePartical != null)
        {
            Instantiate(DiePartical, transform.position, transform.rotation);
        }
            gameObject.GetComponentInChildren<Renderer>().enabled = false;
            movement.canMove = false;
        movement.velocity = Vector3.zero;
            StartCoroutine(ResetPlayer());
        
    }

    public void PickUpCoint()
    {
        Coins += 1;
        CointText.text = "COINS: " + Coins.ToString();
    }
}
