using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotelSafeHandler : BaseSafeHandler
{


    //Models the functionality of a hotel safe, allowing you to enter a code, press the "lock" button to lock the safe with that code. 
    //You can then enter the code you entered and press the "lock" button again to unlock.

    public string keyCode;

    public int maxCodeSize = 6;
    public int minCodeSize = 4;


    void Start()
    {
        base.Initialize();
    }

    public override bool IsLocked()
    {
        return (!string.IsNullOrEmpty(keyCode));
    }

    public override void Enter()
    {
        if (!IsLocked())
        {
            if (!string.IsNullOrEmpty(displayText.text) && displayText.text.Length >= minCodeSize)
            {
                Lock();
            }
            else
            {

                SetDisplayText(errorText);
            }
        }
        else
        {
            if (keyCode == displayText.text)
            {
                Unlock();

            }
            else
            {

                SetDisplayText(failedAttemptText);

            }

        }
    }

    public override void Lock()
    {
        keyCode = displayText.text;
        base.Lock();

    }

    public override void Unlock()
    {
        base.Unlock();
        keyCode = string.Empty;

    }

    public override void NumberPressed(int number)
    {
        base.NumberPressed(number);
        if (displayText.text.Length < maxCodeSize)
        {
            displayText.text += number;
        }
    }
}
