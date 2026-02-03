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

    [Header("시스템 설치 가능 방")]
    [SerializeField] private CRoom[] _rooms;

    [Header("기본 설치 시스템")]
    [SerializeField] private List<CSystem.ESystemType> _eSystems;
    // 컨트롤 도어 센서 메디, 산소, 실드 엔진 무기 등.
    // 추가 순서는 System의 enum 참고
    #endregion

    #region 내부 변수
    //private Dictionary<GameObject, string> childObjects; 
    //
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
        AddSystem(CSystem.ESystemType.Weapons);
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public bool AddSystem(CSystem.ESystemType type)
    {
        // 배열을 탐색해 설치 가능한 방이 남았는지 확인
        // floor 라는 .cs를 따로 만들어야 하는가
        bool success = false;

        for (int i = 0; i < _rooms.Length; i++)
        {
            // 돌면서 빈방에 설치
            if (!_rooms[i].IsExistSystem)
            {
                success = _rooms[i].Install(type);
            }
            if (success) break;
        }
        if (success)
        {
            // 설치 성공
            Debug.Log("아마도 설치 성공");
        }
        else
        {
            // 설치 실패
            Debug.Log("아마도 설치 실패. 인테리어 메니저에 적절한 프리셋이 없거나 빈 방이 없다.");
        }

        // 성공시 대충 다 연결

        // 성공 여부 반환

        return false;
    }
}
