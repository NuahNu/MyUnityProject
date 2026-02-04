using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


#region CRoom
/*
방에 어떤 시스템이 있는지 저장.
방에 몇명까지 들어갈 수 있는지 저장.

누가 들어가있는지 저장.
*/
#endregion

public class CRoom : MonoBehaviour, IPointerDownHandler
{

    #region 인스펙터
    [Header("타입")]
    [SerializeField] private ERoomType _type;

    [Header("문의 위치")]
    [SerializeField] private EDoorPosition _doorPosition;

    [Header("들어가있을 예정인 객체들")]
    [ReadOnly]
    [SerializeField] private List<GameObject> _allys;
    [ReadOnly]
    [SerializeField] private List<GameObject> _enemys;
    // 지금 당장 들어가 있지 않아도 들어갈 예정이라고 예약한다.
    // 따라서 미리 들어가도록 명령이 되어있다면 다른 녀석들이 못 들어가는게 맞다.

    [Header("설치된 시스템 스크립트")]
    [ReadOnly]
    [SerializeField] private CSystem _system;
    #endregion

    #region 내부 변수

    #endregion

    public bool IsExistSystem { get { return _system != null; } }
    public bool IsUseGlow { get { return _system.IsUseGlow; } }

    public ERoomType RoomType { get { return _type; } }

    public int AllyCount { get { return _allys.Count; } }
    public int EnemyCount {  get { return _enemys.Count; } }

    public int RoomSize => _type switch {
        ERoomType.O_2_2 => 4,
        ERoomType.I_2_1 => 2,
        ERoomType.I_1_2 => 2,
        _ => 0
        };

    void Awake()
    {

    }

    void Start()
    {

    }

    void Update()
    {

    }

    public bool EnterRoom(GameObject people)
    {
        if (people.tag == "Ally")
        {
            if (!_allys.Contains(people))
            {
                if (_allys.Count < RoomSize)
                {
                    _allys.Add(people);
                    return true;
                }
                else
                {
                    Debug.LogWarning("인원 초과.");
                }
            }
            else
            {
                Debug.LogWarning("이미 추가된 유닛이다.");// 이게 어떻게 가능한거지?
            }
        }
        else if (people.tag == "Enemy")
        {
            if (!_enemys.Contains(people))
            {
                if (_enemys.Count < RoomSize)
                {
                    _enemys.Add(people);
                    return true;
                }
                else
                {
                    Debug.LogWarning("인원 초과.");
                }
            }
            else
            {
                Debug.LogWarning("이미 추가된 유닛이다.");// 이게 어떻게 가능한거지?
            }
        }
        else
        {
            Debug.LogWarning("태그가 지정되지 않은 유닛이다.");
        }
        return false;
    }

    public void ExitRoom(GameObject people)
    {
        if (people.CompareTag("Ally"))
        {
            _allys.Remove(people);
        }
        else if (people.CompareTag("Enemy"))
        {
            _enemys.Remove(people);
        }
        else
        {
            Debug.LogWarning("태그가 지정되지 않은 유닛이다.");
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        CPlayerInput.Instance.OnPointerDown(eventData);
    }

    public bool CheckEnemy(GameObject gameObject, out GameObject taget)
    {
        taget = null;
        //if (gameObject.TryGetComponent(out CPeopleController pc))
        string tag = gameObject.tag;
        switch (tag)
        {
            case "Ally":
                if (_enemys != null && _enemys.Count > 0)
                {
                    taget = _enemys[0];
                    return true;
                }
                break;
            case "Enemy":
                if (_allys != null && _allys.Count > 0)
                {
                    taget = _allys[0];
                    return true;
                }
                break;
            default: return false;
        }

        return false;
    }

    public bool NeedExtinguish()
    {
        return false;
    }

    public bool NeedRepair()
    {
        return false;
    }


    public CSystem Install(CSystem.ESystemType type)
    {
        // 설치 가능 조건 확인
        CInteriorPreset[] array = CInteriorManager.Instance.DataDic[type];

        CInteriorPreset seleted = null;

        for (int i = 0; i < array.Length; i++)
        {
            //Debug.Log($"{_type} : {array[i].type}");
            //Debug.Log($"{_doorPosition} : {array[i].equipmentPosition}");

            if (_type != array[i].type)   // 방과 시설의 크기가 같고
                continue;
            if (((int)_doorPosition & (int)array[i].equipmentPosition) != 0) // 문과 장비의 위치가 겹치지 않으면 
                continue;

            seleted = array[i];
            break;
        }
        if (seleted == null)
            return null;

        _system = this.gameObject.AddComponent<CSystem>();

        _system.Init(this, seleted, CInteriorManager.Instance.IconDic[type]);

        return _system;
    }
}
