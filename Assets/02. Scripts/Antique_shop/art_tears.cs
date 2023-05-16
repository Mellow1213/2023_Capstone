using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class art_tears : MonoBehaviour
{
    public Material[] artmat = new Material[4]; //바꿀 material 배열

    int i = 0;

    void Update()
    {
        if (Input.GetKeyDown("j")) //'j' 입력(= 심박수가 높아질 때)
        {
            i += 1; //단계에 따라 i증가
            Renderer renderer = GetComponent<Renderer>();
            Material[] materials = renderer.materials;
            materials[1] = artmat[i]; // 두 번째 Material을 새로운 Material로 변경 (첫 번째 = 액자 material)
            renderer.materials = materials;
        }
    }
}
