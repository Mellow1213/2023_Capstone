using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ShowControllers : MonoBehaviour
{
    public bool showController = false; // 컨트롤러를 표시할지 여부를 설정하는 변수
    
    void Update()
    {
        foreach (var hand in Player.instance.hands)
        {
            if (showController)
            {
                hand.ShowController(); // 컨트롤러를 표시하는 함수 호출
            }
            else
            {
                hand.HideController(); // 컨트롤러를 숨기는 함수 호출
            }
        }
        
    }
}
