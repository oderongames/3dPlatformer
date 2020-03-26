using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string LevelToLoadName = "PlatformerLevel";

    //new
    public int SelectedItem;
    public int ItemCount;

    public int index;
    [SerializeField] bool keyDown;
    [SerializeField] int maxIndex;
    public AudioSource audioSource;

    public List<GameObject> MenuObjects = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switchButton();
    }




    public void switchButton()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            if (!keyDown)
            {
                if (Input.GetAxis("Vertical") < 0)
                {
                    if (index < maxIndex)
                    {
                        index++;

                    }
                    else
                    {
                        index = 0;
                    }
                }
                else if (Input.GetAxis("Vertical") > 0)
                {
                    if (index > 0)
                    {
                        index--;
                    }
                    else
                    {
                        index = maxIndex;
                    }
                }

                // alterd


                if (audioSource != null)
                    audioSource.Play();
                // play button animation also
                // ItemInfoText.text = playerInventory[index].Description;


               
                    maxIndex = MenuObjects.Count - 1;
                    MenuObjects[index].gameObject.GetComponent<Button>().Select();

                
            
                keyDown = true;
            }
        }
        else
        {
            keyDown = false;
        }
    }





    public void ExitGame()
    {
        Application.Quit();
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(LevelToLoadName);
    }

    public void WaitLoading()
    {
        // play load menu music
        SceneManager.LoadScene(1);
    }
}
