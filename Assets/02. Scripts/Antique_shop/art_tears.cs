using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class art_tears : MonoBehaviour
{
    public Material[] artmat = new Material[4]; //�ٲ� material �迭

    int i = 0;

    void Update()
    {
        if (Input.GetKeyDown("j")) //'j' �Է�(= �ɹڼ��� ������ ��)
        {
            i += 1; //�ܰ迡 ���� i����
            Renderer renderer = GetComponent<Renderer>();
            Material[] materials = renderer.materials;
            materials[1] = artmat[i]; // �� ��° Material�� ���ο� Material�� ���� (ù ��° = ���� material)
            renderer.materials = materials;
        }
    }
}
