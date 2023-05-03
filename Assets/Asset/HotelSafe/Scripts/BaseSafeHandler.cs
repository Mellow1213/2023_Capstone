using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseSafeHandler : MonoBehaviour
{

    public GameObject keypad;
    public Text displayText;

    public string openText = "Opened";
    public string closedText = "Closed";
    public string failedAttemptText = "Incorrect";
    public string errorText = "Error";


    public AudioSource beepSound;
    public AudioSource safeOpenSound;

    private bool resetOnNextPress = false;



    // Use this for initialization
    void Start()
    {

        Initialize();

    }

    public virtual void Initialize()
    {
        if (displayText == null)
        {
            throw new System.Exception("Safe handler must have a display");
        }

        if (keypad == null)
        {
            throw new System.Exception("Safe handler must have a keypad");
        }

        Clear();

        InitialiseKeypad();
    }

    private void InitialiseKeypad()
    {
        //Get the keys of the keypad
        if (keypad != null)
        {
            foreach (Transform key in keypad.transform)
            {
                ButtonHandler buttonHandler = key.GetComponentInChildren<ButtonHandler>();
                buttonHandler.OnButtonPressed += HandleKeyPress;
            }
        }
    }

    private void HandleKeyPress(ButtonHandler.KeyCodes key)
    {
        if (key == ButtonHandler.KeyCodes.Clear)
        {
            if (beepSound != null)
            {
                beepSound.Play();
            }
            Clear();
        }
        else if (key == ButtonHandler.KeyCodes.Key)
        {
            Enter();
        }
        else
        {
            if (beepSound != null)
            {
                beepSound.Play();
            }
            NumberPressed((int)key);
        }


    }

    public void SetDisplayText(string text)
    {

        displayText.text = text;
        resetOnNextPress = true;
    }


    public virtual void Clear()
    {
        displayText.text = string.Empty;

    }

    public abstract bool IsLocked();

    public abstract void Enter();

    public virtual void NumberPressed(int number)
    {
        if (resetOnNextPress)
        {
            Clear();
            resetOnNextPress = false;
        }
    }


    public virtual void Lock()
    {
        if (safeOpenSound != null)
        {
            safeOpenSound.Play();
        }

        SetDisplayText(closedText);
    }

    public virtual void Unlock()
    {
        if (safeOpenSound != null)
        {
            safeOpenSound.Play();
        }
        SetDisplayText(openText);
    }



}
