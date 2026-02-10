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
    [SerializeField] private List<CSystem.ESystemType> _defaultSystems;
    // 컨트롤 도어 센서 메디, 산소, 실드 엔진 무기 등.
    // 추가 순서는 System의 enum 참고
    // 이거 꼭 List여야 하나?

    [Header("전력")]
    [ReadOnly]
    [SerializeField] private int _maxPower = 40;
    [SerializeField] private int _currentMaxPower = 8;
    [ReadOnly]
    [SerializeField] private int _remainingPower = 8;


    [Header("무기 위치")]
    [SerializeField] private Transform _weaponRoot;
    [SerializeField] private Transform[] _weaponPos;

    [Header("설치된 무기")]
    [SerializeField] private List<CWeapon> _weapons = new();

    [Header("UI확인용")]
    [ReadOnly]
    [SerializeField] private CMainUI _mainUI;
    #endregion

    #region 내부 변수
    //private Dictionary<GameObject, string> childObjects; 
    private readonly Dictionary<CSystem.ESystemType, CSystem> _installedSystem = new();
    #endregion

    public CMainUI MainUI { get { return _mainUI; } set { _mainUI = value; } }

    public Dictionary<CSystem.ESystemType, CSystem> InstalledSystem {  get { return _installedSystem; } }

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

        CheckWeaponRoots();

    }

    void CheckWeaponRoots()
    {
        if (_weaponPos == null || _weaponPos.Length == 0)
        {
            Debug.LogWarning($"At {gameObject.name} : _weaponPos == null || _weaponPos.Length == 0");
            return;
        }

        if (_weaponRoot == null)
        {
            Transform weaponRoot = transform.Find("WeaponRoot");

            if (weaponRoot != null)
            {
                _weaponRoot = weaponRoot;
            }
            else
            {
                Debug.LogWarning($"At {gameObject.name} : _weaponRoot == null");
                return;
            }

            _weaponRoot.localPosition = Vector3.zero;
        }
    }

    void AddDefaultSystem()
    {
        for (int i = 0; i < _defaultSystems.Count; i++)
        {
            AddSystem(_defaultSystems[i]);
        }
    }

    void AddDefaultWeapon()
    {

    }

    void Start()
    {
        AddDefaultSystem();
    }

    void Update()
    {

    }

    public bool AddSystem(CSystem.ESystemType type)
    {
        // 먼저 설치되었느지 확인.
        if (_installedSystem.ContainsKey(type))
        {
            Debug.LogWarning($"{gameObject.name}에 이미 존재하는 시스템을 추가하려고 한다.");
            return false;
        }

        // 배열을 탐색해 설치 가능한 방이 남았는지 확인
        // floor 라는 .cs를 따로 만들어야 하는가
        CSystem newSystem = null;

        for (int i = 0; i < _rooms.Length; i++)
        {
            // 돌면서 빈방에 설치
            if (!_rooms[i].IsExistSystem)
            {
                newSystem = _rooms[i].Install(type);
            }
            if (newSystem != null) break;
        }
        if (newSystem != null)
        {
            // 설치 성공
            Debug.Log($"{type} 아마도 설치 성공");

            _installedSystem.Add(type, newSystem);
        }
        else
        {
            // 설치 실패
            Debug.Log($"{type} 아마도 설치 실패. 인테리어 메니저에 적절한 프리셋이 없거나 빈 방이 없다.");
        }

        // 성공시 대충 다 연결
        MainUI.ChangeFlag(this.tag);
        // 성공 여부 반환

        return false;
    }

    public bool SupplyPower(CSystem.ESystemType type)
    {
        if (_remainingPower == 0) return false;

        if (!_installedSystem.ContainsKey(type)) return false;

        if (_installedSystem[type].MaxPower <= _installedSystem[type].CurrentPower) return false;

        // 시스템의 함수를 부르는것으로 변경한다.
        _installedSystem[type].CurrentPower++;
        _remainingPower--;
        //Debug.Log($"{gameObject.name}의 {type}system의 전력 {_installedSystem[type].CurrentPower}로 상승.");
        return true;
    }
    public bool CutOffSupply(CSystem.ESystemType type)
    {
        if (!_installedSystem.ContainsKey(type)) return false;

        if (0 >= _installedSystem[type].CurrentPower) return false;

        // 시스템의 함수를 부르는것으로 변경한다.
        _installedSystem[type].CurrentPower--;
        _remainingPower++;
        //Debug.Log($"{gameObject.name}의 {type}system의 전력 {_installedSystem[type].CurrentPower}로 감소.");

        return false;
    }
}
