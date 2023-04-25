using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSafeHandler : BaseSafeHandler {

    //Has a preset code which is set in the designer.Clicking the buttons presses them and when the correct code is entered the safe automatically unlocks.
    //Once the safe is open you can click on the safe door to open and close it.

    public string code;
    private bool locked = true;

    public DoorHandler doorHandler;


    // Use this for initialization
    void Start()
    {
        base.Initialize();

        if (doorHandler == null)
        {
            throw new System.Exception("A door handler is required");
        }

        if (string.IsNullOrEmpty(code))
        {
            throw new System.Exception("A code is required");
        }
    }

    public override void Enter()
    {
        if (!locked && !doorHandler.isOpen) {
            locked = true;
            Lock();
        }
    }

    public override bool IsLocked()
    {
        return locked;
    }

    public override void Clear()
    {
        if (locked)
        {
            base.Clear();
        }
    }

    public override void NumberPressed(int number)
    {
        if (locked)
        {
            base.NumberPressed(number);

            if (displayText.text.Length < code.Length)
            {
                displayText.text += number;
            }

            if (displayText.text.Length == code.Length)
            {
                if (displayText.text == code)
                {
                    //open safe
                    locked = false;
                    Unlock();
                }
                else
                {
                    SetDisplayText(failedAttemptText);
                }
            }
        }
    }
}
