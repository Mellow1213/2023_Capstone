using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles button presses on the keypad and passes the key pressed
/// up to the safe controller.
/// </summary>
public class ButtonHandler : MonoBehaviour {

    public delegate void KeypadButtonPressed(KeyCodes key);

    public event KeypadButtonPressed OnButtonPressed;
    public KeyCodes keyCode;
    public enum KeyCodes {
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Clear = 10,
        Key= 11
    }

    Animator buttonAnimator;
    private void Start()
    {
        buttonAnimator = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        ButtonPressed();
    }

    private void ButtonPressed() {
        if(buttonAnimator != null) { 
            buttonAnimator.Play(GetAnimationName(), -1, 0f);
        }

        if (OnButtonPressed != null) {
            OnButtonPressed(keyCode);
        }
    }

    private string GetAnimationName() {

        switch (keyCode) {
            case KeyCodes.Zero: return "Button0";
            case KeyCodes.One: return "Button1";
            case KeyCodes.Two : return "Button2";
            case KeyCodes.Three: return "Button3"; 
            case KeyCodes.Four: return "Button4";
            case KeyCodes.Five: return "Button5";
            case KeyCodes.Six: return "Button6";
            case KeyCodes.Seven: return "Button7";
            case KeyCodes.Eight: return "Button8"; 
            case KeyCodes.Nine: return "Button9";
            case KeyCodes.Clear: return "ButtonClear";
            case KeyCodes.Key: return "ButtonKey";
        }
        Debug.Log("KeyCode not recognised");
        return null;
    }

}
