
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

namespace Valve.VR.InteractionSystem.Sample {
    public class ButtonAttach : MonoBehaviour
    {
        public delegate void KeypadButtonPressed(KeyCodes key);

        public event KeypadButtonPressed OnButtonPressed;
        public KeyCodes keyCode;
        public enum KeyCodes
        {
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
            Key = 11
        }

        public void OnButtonDown(Hand fromHand)
        {
            ButtonPressed();
            fromHand.TriggerHapticPulse(1000);
        }

        private void ButtonPressed()
        {
            if (OnButtonPressed != null)
            {
                OnButtonPressed(keyCode);
            }
        }

        
    }

}
