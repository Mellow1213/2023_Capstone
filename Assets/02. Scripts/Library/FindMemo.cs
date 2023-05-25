using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindMemo : MonoBehaviour
{
    public GameObject safe;//Safe 오브젝트를 참조하기 위한 변수
    public Text display; //Safe 오브젝트의 Animator 컴포넌트를 참조하기 위한 변수
    private Animator ani; //Safe 오브젝트의 Animator 컴포넌트를 참조하기 위한 변수
    public bool isFind = false; //힌트를 찾았는지 여부를 나타내는 변수

    // Start is called before the first frame update
    void Start()
    {
        ani = safe.GetComponent<Animator>();//시작할 때 Safe 오브젝트의 Animator 컴포넌트를 가져옴
    }

    public void FindHintEvent()//memo를 grab
    {
        //힌트를 찾았을 때 호출되는 함수. Safe의 문을 열기 위해 애니메이션 트리거를 설정하고,
        //비밀번호를 UI 텍스트에 표시. 또한 isFind 변수를 true로 설정하여 DownMannequin의 분해 이벤트 실행.
        ani.SetTrigger("openDoor");
        display.text = "5214";
        isFind = true;
    }
}
