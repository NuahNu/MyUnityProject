using System.Collections.Generic;
using UnityEngine;


#region CShip
/*
상속 시키거나
인스펙터값을 변경하거나- 이 경우 프리셋 같은걸 
 ㄴ 이것도 디테일인가
  ㄴ 그럼 그냥 일단 만들기
깊게 생각하지 말기? 어떻Crab

배는 클릭 가능한 객체가 아니다.

base 이미지와
floor (방) 정보가 있어야 한다.
체력은 누가 갖게 하는게 좋을까?

체력은 다 같지만?, 보호막은 여부에 따라 다르다..

각 자식들을 그릴지 말지도 정할 수 있다.

*/
#endregion

public class CShip : MonoBehaviour
{
    #region 인스펙터
    [Header("자식 오브젝트")]
    [SerializeField] private GameObject _base;
    [SerializeField] private GameObject _floor;
    //[SerializeField] private GameObject _ui;
    #endregion

    #region 내부 변수
    //private Dictionary<GameObject, string> childObjects; 
    #endregion

    private void Reset()
    {

    }

    void Awake()
    {
        if (tag != "Ally" && tag != "Enemy")
        {
            Debug.LogError("함선의 태그를 꼭 선택해주세요. Ally or Enemy");
            return;
        }
        Transform[] transforms = GetComponentsInChildren<Transform>();
        foreach (Transform childrentransform in transforms)
        {
            childrentransform.gameObject.tag = this.gameObject.tag;
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
