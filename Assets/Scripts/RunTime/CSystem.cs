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
    // 우선순위, UI 출력순서도 겸한다.
    public enum ESystemType
    {
        Shields,
        Engines,
        Oxygen,
        Weapons,
        Drones,
        Medbay,
        Pilot,
        Sensors,
        Door,
        Cloaking,
        Teleporter,
        Count
    }
    #region 인스펙터
    // 그려낼 프리팹 - 타일맵 위에 보이는건 여기서?
    [Header("인테리어 프리팹 ( 함선에서 추가하면 자동으로 설정됨. )")]
    [ReadOnly]
    [SerializeField] private CInterior _interior;

    [Header("시스템 타입")]
    [ReadOnly]
    [SerializeField] private ESystemType _systemType;
    // UI 를 담당할 친구 하나 추가.


    // 연결된 방
    // 
    // 
    #endregion

    #region 내부 변수
    // 레벨
    // 소속된 방.
    private CRoom _room;
    #endregion

    public bool IsExistInterior { get { return _interior != null; } }

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
        if (_room.NeedExtinguish() || _room.NeedRepair())
        {
            _interior.ChangeState(CInterior.EInterState.Disabled);
        }
        else if (0 < (gameObject.tag == "Ally" ? _room.AllyCount : _room.EnemyCount))
        {
            _interior.ChangeState(CInterior.EInterState.Online);
        }
        else
        {
            _interior.ChangeState(CInterior.EInterState.Offline);
        }
    }

    internal void Init(CRoom cRoom, CInteriorPreset seleted)
    {
        // 방 연결
        _room = cRoom;

        GameObject interiorGO = new GameObject("Interior");
        interiorGO.transform.parent = this.transform;
        interiorGO.transform.localPosition = new Vector3(0, 0, Common.z_offset);
        interiorGO.tag = this.gameObject.tag;

        _interior = interiorGO.AddComponent<CInterior>();
        _interior.Preset = seleted;
    }
}
