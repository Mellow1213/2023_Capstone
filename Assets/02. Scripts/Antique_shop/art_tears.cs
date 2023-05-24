using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class art_tears : MonoBehaviour
{
    public Material[] artmat = new Material[4]; //바꿀 material 배열

    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        Material[] materials = renderer.materials;
        materials[1] = artmat[FTU.Instance.BPMEvent]; // 두 번째 Material을 새로운 Material로 변경 (첫 번째 = 액자 material)
        renderer.materials = materials;
    }

}