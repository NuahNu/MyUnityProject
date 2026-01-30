using UnityEngine;


#region CSystem
/*

일반과 sUB

얘는 또 
지 정보
floor 에 그려질 
    인테리어
    아이콘
ui
    게임창
    업그레이드 - ㅂㄹ

**그냥 컴퍼넌트로 추가하면 지가 알아서 붙도록.**
*/
#endregion

public class CSystem : MonoBehaviour
{
    public enum ESystemType
    {

    }
    #region 인스펙터
    // 그려낼 프리팹
    [Header("인테리어 프리팹")]
    [SerializeField] private CInterior interior;
    #endregion

    #region 내부 변수
    // 레벨
    // 소속된 방.
    #endregion

    void Awake()
    {
        // 인테리어 프리팹은 방의 구조에 따라서 다른걸 가져온다.
        // 문의 위치에 따라서 다른 프리팹을 가져와야한다.
        // enumFlag로 방과 비교해 걸리는게 없어야 가능하다.
        // 시스템의 종류와 방의 정보를 갖고 설치를 시도한다.
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
