using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    // Add the platform that you want to activate to the slot
    // Add the Button On And button off gameobjects to the slot make sure the button off button is set
    // if it is a pushable object trigger set the tag of the pushable object to "Pushable"
    [Header("Buttons to switch when triggerd")]
    public GameObject OffButtonObject;
    public GameObject OnButtonObject;

    public GameObject Platform;

    public bool CanActivateSwitch;


    public enum KeyTriggerType
    {
        KeyHole,
        Button,
        PressurePlate,
        PushableObjectTrigger
    }

    public KeyTriggerType type;
    public KeyCode TriggerButton = KeyCode.E;
    public bool canActivate;

    public AudioSource SwitchSound;
    // Start is called before the first frame update
    void Start()
    {
        OffButtonObject.gameObject.SetActive(true);
        OnButtonObject.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canActivate == true)
        {
            if (Input.GetKeyDown(TriggerButton))
            {
                if (SwitchSound != null)
                    SwitchSound.Play();
                // activate platform
                Platform.gameObject.GetComponentInChildren<Platform>().Activated = true;
                SwitchButtonObjects();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //  canActivate = true;
        if (type == KeyTriggerType.PressurePlate)
        {
            if (other.gameObject.tag == "Player")

                if (SwitchSound != null)
                    SwitchSound.Play();

            Platform.gameObject.GetComponentInChildren<Platform>().Activated = true;
            SwitchButtonObjects();
        }
        else if (type == KeyTriggerType.PushableObjectTrigger)
        {
            if (other.gameObject.tag == "Pushable")
            {
                if (SwitchSound != null)
                    SwitchSound.Play();

                Platform.gameObject.GetComponentInChildren<Platform>().Activated = true;
                SwitchButtonObjects();
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        canActivate = true;

        // create player display to activate button
        Debug.Log("Press Button to activate trigger");
    }
    private void OnTriggerExit(Collider other)
    {
        canActivate = false;
    }

    public void SwitchButtonObjects()
    {
        // play sound effect
        OffButtonObject.gameObject.SetActive(false);
        OnButtonObject.gameObject.SetActive(true);
    }
}
