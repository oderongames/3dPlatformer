using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // in animator  change update time from normal to unscaled time to prevent nothing from happening in uncald time


    public static bool GameIsPaused = false;



    public string currentLevelName = "Level Name";
    //new
    public int SelectedItem;
    public int ItemCount;
    

    public int index;
    [SerializeField] bool keyDown;
    [SerializeField] int maxIndex;
    public AudioSource audioSource;

    public GameObject PauseMenuObject;
    public List<GameObject> MenuObjects = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        PauseMenuObject.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switchButton();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();

            }
            else
            {
                Pause();
            }
        }

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








    public void RestartLevel()
    {
        Time.timeScale = 1f;
        // play load menu music
        SceneManager.LoadScene(currentLevelName);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        // play load menu music
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        
        PauseMenuObject.gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }

    public void Pause()
    {
        PauseMenuObject.gameObject.SetActive(true);
        Time.timeScale = 0.00001f;
        GameIsPaused = true;
    }
}
